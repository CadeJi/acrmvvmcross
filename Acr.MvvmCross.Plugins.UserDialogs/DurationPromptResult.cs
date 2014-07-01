﻿using System;


namespace Acr.MvvmCross.Plugins.UserDialogs {
    
    public class DurationPromptResult {

        public bool Success {
            get { return (this.SelectedTimeSpan != null); }
        }

        public TimeSpan? SelectedTimeSpan { get; private set; }



        public DurationPromptResult(TimeSpan? selectedTimeSpan) {
            this.SelectedTimeSpan = selectedTimeSpan;
        }
    }
}
