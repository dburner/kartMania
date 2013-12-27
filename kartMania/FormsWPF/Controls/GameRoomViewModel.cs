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
using System.Linq;
using kartManiaCommons.Structs;

namespace kartMania.FormsWPF.Controls
{
	/// <summary>
	/// Description of GameRoomsListViewModel.
	/// </summary>
	public class GameRoomViewModel : INotifyPropertyChanged
	{
		ObservableCollection<GameRoomInfo> m_gameRooms = new ObservableCollection<GameRoomInfo>();
		
		public GameRoomViewModel()
		{
		}
		
		#region Properties
		
		public ObservableCollection<GameRoomInfo> GameRooms
		{
			get { return m_gameRooms; }
		}
		
		public void AddGameRoom(GameRoomInfo gri)
		{
			m_gameRooms.Add(gri);
			
			RaisePropertyChanged("GameRooms");
		}
		
		public void RemoveGameRoom(GameRoomInfo gri)
		{
			GameRoomInfo toDelete = m_gameRooms.First( s => s.gameRoomId == gri.gameRoomId );
			
			m_gameRooms.Remove(toDelete);
		}

		
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
