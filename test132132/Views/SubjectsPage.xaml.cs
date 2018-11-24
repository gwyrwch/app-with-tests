using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace test132132
{
    public partial class SubjectsPage : ContentPage
    {
        ViewModels.Tests.SubjectsViewModel viewModel;
        public SubjectsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ViewModels.Tests.SubjectsViewModel();

            RenderTableView();
            //CSharpTableSection.BindingContext = viewModel.subjects; //todo ???

            //SubjectsListView.ItemsSource = App.Subjects;
            //SubjectsTableView.Resources = App.Subjects;
        }

        void RenderTableView() {
            // viewModel.SplitBySubject сейчас возвращает все что нужно отрендерить
            // создать TableSection-ов и в каждый перебить в ListView его группу
            SubjectsTableView.Root.Clear();
            var groups = viewModel.testPreview.SplitBySubject();
            TableSection lastSection = null;

            foreach (IGrouping<string, Models.Test> group in groups) {
                string subject = group.Key;
                IEnumerable<Models.Test> unpacked = group.AsEnumerable();

                var section = new TableSection(subject);
                lastSection = section;
                int count = 0;
                foreach (var test in unpacked) {
                    count += 1;
                    section.Add(new Common.CustomViewCell(test, count));
                }
                SubjectsTableView.Root.Add(section);
            }

            if (lastSection != null) {
                lastSection.Add(new Common.CenteredTextCell(string.Format("Needed time: {0}. Questions to answer: {1}",
                        viewModel.testPreview.CountRequiredTime().ToString(),
                        viewModel.testPreview.CountUnansweredQuestions().ToString()),
                        Color.FromHex("#bdc3c7")
                ));

            } else {
                lastSection = new TableSection();
                lastSection.Add(new Common.CenteredTextCell("No matches", Color.FromHex("#007aff")));
                SubjectsTableView.Root.Add(lastSection);
            }
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            // todo
            //var item = args.SelectedItem as string;
            //if (item == null)
            //  return;

            await Navigation.PushAsync(new AboutPage());

           // SubjectsListView.SelectedItem = null;
        }

        void SearchText_Changed(object sender, EventArgs e)
        {
            viewModel.testPreview.LoadAll();
            viewModel.testPreview.SearchByTitle(searchEntry.Text);
            RenderTableView();
        }


        async void Sort_Clicked(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet("How to display?", "Cancel", 
                                                  null, "A-Z", "Z-A", 
                                                  "Small first", "Large first");

            switch (action)
            {
                case "A-Z":
                    {
                        viewModel.testPreview.LoadAll();
                        viewModel.testPreview.SortByAlphabet();
                        RenderTableView();
                        break;
                    }
                case "Z-A":
                    {
                        viewModel.testPreview.LoadAll();
                        viewModel.testPreview.SortByAlphabetDescending();
                        RenderTableView();
                        break;
                    }
                case "Small first":
                    {
                        viewModel.testPreview.LoadAll();
                        viewModel.testPreview.SortByCount();
                        RenderTableView();
                        break;
                    }
                case "Large first":
                    {
                        viewModel.testPreview.LoadAll();
                        viewModel.testPreview.SortByCountDescending();
                        RenderTableView();
                        break;
                    }
                case "Cancel" :
                    return;
            }
        }


        async void Filter_Clicked(object sender, EventArgs e)
        {
            int max = viewModel.testPreview.TestsToPreview.Max(test =>
            {
                if(test.Mode == Models.TimeMode.limitForQuestion)
                    return (int)test.TimeLimit.Value.TotalMinutes * test.Count;
                return (int)test.TimeLimit.Value.TotalMinutes;
            });

            int min = viewModel.testPreview.TestsToPreview.Min(test =>
            {
                if (test.Mode == Models.TimeMode.limitForQuestion)
                    return (int)test.TimeLimit.Value.TotalMinutes * test.Count;
                return (int)test.TimeLimit.Value.TotalMinutes;
            });

            string shortTests = null, largeTests = null;
            int leftS = min, rightS = max, leftL = 0, rightL = 0;

            if (max != min)
            {
                if(max <= 10) 
                {
                    shortTests = string.Format("Short tests {0}-{1} min", min, max);
                    largeTests = null;
                    rightS = max;
                }
                else if(max >= 10)
                {
                    shortTests = string.Format("Short tests {0}-{1} min",
                                                min, (max + min) / 2
                                    );
                    largeTests = string.Format("Long tests {0}-{1} min",
                                               (max + min) / 2, max
                                    );
                    rightS = (max + min) / 2;
                    leftL = (max + min) / 2 + 1;
                    rightL = max;
                }
            } else {
                shortTests = null; 
                largeTests = null; 
            }

            List<string> list = App.Subjects.ToList();
            list.AddRange(new List<string> { shortTests, largeTests });

            var action = await DisplayActionSheet("What to display?", "Cancel", 
                                                  null, list.ToArray());

            if (action == shortTests)
            {
                viewModel.testPreview.LoadAll();
                viewModel.testPreview.TimeFilter(leftS, rightS);
                RenderTableView();
            }
            else if(!string.IsNullOrEmpty(largeTests) && action == largeTests)
            {
                viewModel.testPreview.LoadAll();
                viewModel.testPreview.TimeFilter(leftL, rightL);
                RenderTableView();
            }
            else if(App.Subjects.Any(x => x == action))
            {
                viewModel.testPreview.LoadAll();
                viewModel.testPreview.SubjectsFilter(action);
                RenderTableView();
            }
            else if (action == "Cancel")
                return;
        }
    }

}
