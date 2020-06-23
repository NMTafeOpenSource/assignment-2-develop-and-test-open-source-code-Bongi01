using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apex.MVVM;

namespace Car_Rental_Program
{
    /// <summary>
    /// The MainViewModel. This is the main view model for the application.
    /// </summary>
    public class MainViewModel : ViewModel
    {
        /// <summary>
        /// The Character notifying property.
        /// </summary>
        private NotifyingProperty RentalProperty =
          new NotifyingProperty("Rental Choice", typeof(RentalType), RentalType.PerDay);

        /// <summary>
        /// Gets or sets the character.
        /// </summary>
        /// <value>
        /// The character.
        /// </value>
        public RentalType RentalType
        {
            get { return (RentalType)GetValue(RentalProperty); }
            set { SetValue(RentalProperty, value); }
        }
    }
}
