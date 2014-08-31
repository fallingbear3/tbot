using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using tbot.Annotations;

namespace tbot.bot{
    public class ProfileManager : INotifyPropertyChanged{
        public ProfileManager(){
            // If we did use List instead of ObservableCollection, binded combobox would not be update.
            Profiles = new ObservableCollection<Profile>(read());
        }

        public ObservableCollection<Profile> Profiles { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        private List<Profile> read(){
            using (var r = new StreamReader(new FileStream("profiles.json", FileMode.OpenOrCreate))){
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<List<Profile>>(json) ?? new List<Profile>();
            }
        }

        internal void add(Profile profile){
            Profiles.Add(profile);
            OnPropertyChanged("Profiles");
            save();
        }

        private void save(){
            string json = JsonConvert.SerializeObject(Profiles.ToArray());
            File.WriteAllText("profiles.json", json);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null){
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}