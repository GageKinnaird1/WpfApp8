﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>

    public partial class Page1 : Window
    {
        private MainWindow main;
        public Page1()
        {
            InitializeComponent();
            main = new MainWindow(this);
            WindowState = WindowState.Maximized;
        }
        
    private void Button_Click(object sender, RoutedEventArgs e)
        {
            main.Show();
            this.Hide();
            WindowState = WindowState.Maximized;


        }
    }
}
