﻿using System;
using System.Collections.Generic;
using System.Linq;

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

        public void RenderTableView() 
        {
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
                    var viewCell = new Common.CustomViewCell(test, count);
                    viewCell.Tapped += ViewCell_Tapped;

                    section.Add(viewCell);
                }
                SubjectsTableView.Root.Add(section);
            }

            if (lastSection != null) {
                lastSection.Add(new Common.CenteredTextCell(
                    string.Format(iOS.AppResources.SubjectsSummaryTemplate,
                    viewModel.testPreview.CountRequiredTime().ToString(),
                    viewModel.testPreview.CountUnansweredQuestions().ToString()),
                    (Color)Application.Current.Resources["Silver"]
                ));
            } else {
                lastSection = new TableSection();
                lastSection.Add(new Common.CenteredTextCell(iOS.AppResources.SubjectsNoMatches, (Color)Application.Current.Resources["MainColor"]));
                SubjectsTableView.Root.Add(lastSection);
            }
        }

        async void ViewCell_Tapped(object sender, EventArgs e)
        {
            var viewCell = (Common.CustomViewCell)sender;
            await Navigation.PushAsync(new Views.TestPreview.TestChosedPage(viewCell.test));
        }

        void SearchText_Changed(object sender, EventArgs e)
        {
            viewModel.testPreview.LoadAll();
            viewModel.testPreview.SearchByTitle(searchEntry.Text);
            RenderTableView();
        }

        readonly string[] options = {
            iOS.AppResources.SubjectsAZ,
            iOS.AppResources.SubjectsZA,
            iOS.AppResources.SubjectsSmallFirst,
            iOS.AppResources.SubjectsLargeFirst
        };

        async void Sort_Clicked(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet(
                iOS.AppResources.SubjectsHowToDisplay, iOS.AppResources.CommonCancel, null, options
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
            else if (action == iOS.AppResources.CommonCancel)
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
                if (max <= 10) 
                {
                    shortTests = string.Format(iOS.AppResources.SubjectsShortTestsTemplate, min, max);   
                    largeTests = null;
                    rightS = max;
                } else if (max >= 10)
                {
                    shortTests = string.Format(
                        iOS.AppResources.SubjectsShortTestsTemplate,  
                        min, (max + min) / 2
                    );
                    largeTests = string.Format(
                        iOS.AppResources.SubjectsLongTestsTemplate,  
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
                iOS.AppResources.SubjectsWhatToDisplay, iOS.AppResources.CommonCancel, 
                null, list.ToArray()
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
            else if (action == iOS.AppResources.CommonCancel)
                return;
        }
    }

}
