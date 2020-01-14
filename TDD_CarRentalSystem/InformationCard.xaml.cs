using System;
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
using LiveCharts;

namespace TDD_CarRentalSystem
{
    /// <summary>
    /// Interaction logic for InformationCard.xaml
    /// </summary>
    public partial class InformationCard : UserControl
    {
        public InformationCard()
        {
            InitializeComponent();
            CardGrid.DataContext = this;
        }

        public SeriesCollection SeriesData
        {
            get { return null; }
            set { SetValue(SeriesDataProperty, value); }
        }

        public static readonly DependencyProperty SeriesDataProperty =
            DependencyProperty.Register(
                "SeriesData",
                typeof(SeriesCollection),
                typeof(InformationCard),
                new PropertyMetadata(null));

        #region BackgroundColour DP

        public string BackgroundColour
        {
            set { SetValue(ColourBG, value); }
        }

        public static readonly DependencyProperty ColourBG =
            DependencyProperty.Register(
                "BackgroundColour",
                typeof(string),
                typeof(InformationCard),
                new PropertyMetadata(""));

        #endregion

        #region BorderColour DP

        public string BorderColour
        {
            set { SetValue(ColourB, value); }
        }

        public static readonly DependencyProperty ColourB =
            DependencyProperty.Register(
                "BorderColour",
                typeof(string),
                typeof(InformationCard),
                new PropertyMetadata(""));

        #endregion

        #region BorderWidth DP

        public string BorderWidth
        {
            set { SetValue(WidthB, value); }
        }

        public static readonly DependencyProperty WidthB =
            DependencyProperty.Register(
                "BorderWidth",
                typeof(string),
                typeof(InformationCard),
                new PropertyMetadata(""));

        #endregion

        #region Title DP

        public string Title
        {
            get { return (String)GetValue(titleDP); }
            set { SetValue(titleDP, value); }
        }

        public static readonly DependencyProperty titleDP =
            DependencyProperty.Register(
                "Title",
                typeof(string),
                typeof(InformationCard),
                new PropertyMetadata(""));

        #endregion

        #region lblValue DP

        public string ValueProperty
        {
            set { SetValue(valueDP, value); }
        }

        public static readonly DependencyProperty valueDP =
            DependencyProperty.Register(
                "ValueProperty",
                typeof(string),
                typeof(InformationCard),
                new PropertyMetadata(""));

        #endregion
    }
}
