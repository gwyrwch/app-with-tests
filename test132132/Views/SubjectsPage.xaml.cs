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
        }

        void RenderTableView() {
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
                lastSection.Add(new Common.CenteredTextCell(string.Format("Needed time: {0}. Questions to answer: {1}", // fixme
                        viewModel.testPreview.CountRequiredTime().ToString(),
                        viewModel.testPreview.CountUnansweredQuestions().ToString()),
                        Color.FromHex("#bdc3c7")
                ));

            } else {
                lastSection = new TableSection();
                lastSection.Add(new Common.CenteredTextCell("No matches", Color.FromHex("#007aff"))); // fixme
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

        readonly string[] options = {"A-Z", "Z-A", "Small first", "Large first"}; // fixme

        async void Sort_Clicked(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet(
                "How to display?", "Cancel", null, options // fixme 
            );

            viewModel.testPreview.LoadAll();

            if (action == options[0])
                viewModel.testPreview.SortByAlphabet();
            else if (action == options[1])
                viewModel.testPreview.SortByAlphabetDescending();
            else if (action == options[2])
                viewModel.testPreview.SortByCount();
            else if (action == options[3])
                viewModel.testPreview.SortByCountDescending();
            else if (action == "Cancel")
                return;

            RenderTableView();
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
                    shortTests = string.Format("Short tests {0}-{1} min", min, max);    //fixme
                    largeTests = null;
                    rightS = max;
                }
                else if(max >= 10)
                {
                    shortTests = string.Format("Short tests {0}-{1} min",   // fixme
                                                min, (max + min) / 2
                                    );
                    largeTests = string.Format("Long tests {0}-{1} min",   // fixme 
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

            var action = await DisplayActionSheet(
                "What to display?", "Cancel", null, list.ToArray()   // fixme
            );

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
