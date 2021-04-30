using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace wifiAnalysis
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanPage : ContentPage
    {
        List<RoomObject> Rooms;
        private int RoomSelected;
        public ScanPage()
        {
            InitializeComponent();
            PopulatePicker();
        }

        private async void ScanButton_OnClicked(object sender, EventArgs e)
        {
            if (RoomSelected != -1)
            {
                await Navigation.PushAsync(new ScanProgressPage());
            }
        }

        void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
            int Room_ID = -1;
            if (selectedIndex != -1)
            {
                Room_ID = Rooms.Find(x => x.Room_Name.Equals(picker.Items[selectedIndex])).Room_ID;
                if (Room_ID != -1)
                {
                    RoomSelected = Room_ID;
                }
            }
        }

        private  void PopulatePicker()
        {
            Rooms = Database.GetRooms();
            location.Items.Clear();
            foreach (RoomObject room in Rooms)
            {
                location.Items.Add(room.Room_Name);
            }
        }
    }
}