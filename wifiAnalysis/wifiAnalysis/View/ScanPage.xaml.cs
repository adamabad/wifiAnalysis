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

        private async void PopulatePicker()
        {
            Rooms = await App.ScanDatabase.GetRooms();
            RoomPicker.Items.Clear();
            foreach (RoomObject room in Rooms)
            {
                RoomPicker.Items.Add(room.Room_Name);
            }
            RoomPicker.Items.Add("Other");
        }
    }
}