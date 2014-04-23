﻿using System;
using System.Collections.Generic;


namespace Acr.MvvmCross.Plugins.UserDialogs {
    
    public class ActionSheetOptions {

        public string Title { get; set; }
        //public SheetOption Cancel { get; set; }
        public IList<SheetOption> Options { get; set; } 


        public ActionSheetOptions SetTitle(string title) {
            this.Title = title;
            return this;
        }


        //public ActionSheetOptions SetCancel(string text = "Cancel", Action action = null) {
        //    this.Cancel = new SheetOption(text, action);
        //    return this;
        //}


        public ActionSheetOptions Add(string text, Action action = null) {
            this.Options.Add(new SheetOption(text, action));
            return this;
        }
    }
}
