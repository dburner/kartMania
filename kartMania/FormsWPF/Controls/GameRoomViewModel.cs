/*
 * Created by SharpDevelop.
 * User: Ionut
 * Date: 10/05/2013
 * Time: 20:14 
 * 
 */
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace kartMania.FormsWPF.Controls
{
	/// <summary>
	/// Description of GameRoomsListViewModel.
	/// </summary>
	public class GameRoomViewModel : INotifyPropertyChanged
	{
		
		
		public GameRoomViewModel()
		{
		}
		
		#region Properties
		
				

		
		#endregion
		
		#region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            // take a copy to prevent thread issues
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
	}
}
