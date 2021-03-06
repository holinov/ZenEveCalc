﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Zen.EveCalc.Controls;

namespace Zen.EveCalc
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IPageControl[] _pages;

        public MainWindow()
        {
            InitializeComponent();
            _pages = App.Core.Resolve<IEnumerable<IPageControl>>().OrderBy(p => p.SortOrder).ToArray();
            foreach (var page in _pages)
            {
                try
                {
                    page.PageSope = App.Core.BeginScope();
                    page.Init();
                    var tab = new TabItem()
                        {
                            Header = page.Header,
                            Content = page.PageContent,
                            Tag = page
                        };
                    TabHost.Items.Add(tab);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }

            }

            TabHost.SelectionChanged += (s, o) =>
                {
                    if (o.AddedItems.Count == 1)
                    {
                        var tab = o.AddedItems[0] as TabItem;
                        if (tab != null)
                        {
                           
                            PageToolbar.Items.Clear();
                            var page = (IPageControl) tab.Tag;
                            page.Show();
                            if (page.Commands != null)
                                foreach (var cmd in page.Commands)
                                {
                                    if (cmd.Content != null)
                                    {
                                        PageToolbar.Items.Add(cmd.Content);
                                    }
                                    else
                                    {
                                        var button = new Button()
                                            {
                                                Content = cmd.Name
                                            };
                                        if (cmd.Command != null)
                                        {
                                            button.Command = cmd.Command;
                                        }
                                        else
                                        {
                                            PageCommand cmd1 = cmd;
                                            button.Click += (sender, e) => cmd1.Action(page);
                                        }
                                        PageToolbar.Items.Add(button);
                                    }
                                }
                        }
                    }
                };

            TabHost.SelectedIndex = 0;
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            App.Core.Dispose();
        }
    }
}