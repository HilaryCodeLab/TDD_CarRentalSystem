using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apex.MVVM;

namespace TDD_CarRentalSystem
{
    /// <summary>
    /// The MainViewModel. This is the main view model for the application.
    /// </summary>
    public class MainViewModel : ViewModel
    {
        /// <summary>
        /// The Character notifying property.
        /// </summary>
        private NotifyingProperty BookingProperty =
          new NotifyingProperty("BookingChoice", typeof(BookingType), BookingType.PerDay);

        /// <summary>
        /// Gets or sets the character.
        /// </summary>
        /// <value>
        /// The character.
        /// </value>
        public BookingType BookingType
        {
            get { return (BookingType)GetValue(BookingProperty); }
            set { SetValue(BookingProperty, value); }
        }
    }
}
