using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BWHelper.Properties;

namespace BWHelper
{
    public class ProcessSettings
    {
        private Settings _userSettings;
        private static HashSet<string> _propInClass;

        static ProcessSettings()
        {
            _propInClass =
                new HashSet<string>(
                typeof(ProcessSettings).GetProperties()
                .Select(p => p.Name));
        }

        public ProcessSettings()
        {
            _userSettings = Settings.Default;
            _userSettings.PropertyChanged += userSettings_PropertyChanged; 
        }

        private void userSettings_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (_propInClass.Contains(e.PropertyName))
                RaiseChangedEvent();
        }

        public bool NegativeColor { get { return _userSettings.NegativeColor; } set { if(_userSettings.NegativeColor != value) _userSettings.NegativeColor = value; } }
        public bool NegativeColorOffWithColorPage { get { return _userSettings.NegativeColorOffWithColorPage; }
            set { if(_userSettings.NegativeColorOffWithColorPage != value) _userSettings.NegativeColorOffWithColorPage = value; } }
        public bool NegativeColorOffWithPictureArea { get { return _userSettings.NegativeColorOffWithPictureArea; }
            set { if (_userSettings.NegativeColorOffWithPictureArea != value)_userSettings.NegativeColorOffWithPictureArea = value; } }

        public event EventHandler Changed;


        public bool ReduceLight { get { return _userSettings.ReduceLight; } set {
                if (_userSettings.ReduceLight != value) _userSettings.ReduceLight = value; } } 
        public bool ReduceLightOffWithColorPage { get { return _userSettings.ReduceLightOffWithColorPage; }
            set { if(_userSettings.ReduceLightOffWithColorPage != value) _userSettings.ReduceLightOffWithColorPage = value; } } 
        public bool ReduceLightOffWithPictureArea { get { return _userSettings.ReduceLightOffWithPictureArea; }
            set { if(_userSettings.ReduceLightOffWithPictureArea != value) _userSettings.ReduceLightOffWithPictureArea = value; } }
        public int ReduceLightRedPercent { get { return _userSettings.ReduceLightRedPercent; }
            set { if (_userSettings.ReduceLightRedPercent != value) _userSettings.ReduceLightRedPercent = value; } }
        public int ReduceLightGreenPercent { get { return _userSettings.ReduceLightGreenPercent; }
            set { if(_userSettings.ReduceLightGreenPercent != value) _userSettings.ReduceLightGreenPercent = value; } }
        public int ReduceLightBluePercent { get { return _userSettings.ReduceLightBluePercent; }
            set { if(_userSettings.ReduceLightBluePercent != value) _userSettings.ReduceLightBluePercent = value; } }

        private void RaiseChangedEvent() => Changed?.Invoke(this, EventArgs.Empty);
    }
}
