﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;


namespace Acr.MvvmCross.Plugins.Settings {
    
    public abstract class AbstractSettingsService : ISettingsService {

        public IDictionary<string, string> All { get; private set; }


        protected AbstractSettingsService() {
            var observable = new ObservableDictionary<string, string>(this.GetAllSettings());
            observable.CollectionChanged += this.OnCollectionChanged;
            this.All = observable;
        }

        #region Internals

        protected abstract IDictionary<string, string> GetAllSettings(); 
        protected abstract void SaveSetting(string key, string value);
        protected abstract void RemoveSetting(string key);
        protected abstract void ClearSettings();


        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            switch (e.Action) {
                case NotifyCollectionChangedAction.Add:
                case NotifyCollectionChangedAction.Replace:
                    var saves = e.NewItems.Cast<KeyValuePair<string, string>>();
                    foreach (var item in saves)
                        this.SaveSetting(item.Key, item.Value);
                    break;

                case NotifyCollectionChangedAction.Remove:
                    var dels = e.OldItems.Cast<KeyValuePair<string, string>>();
                    foreach (var item in dels)
                        this.RemoveSetting(item.Key);
                    break;

                case NotifyCollectionChangedAction.Reset:
                    this.ClearSettings();
                    break;
            }
        }

        #endregion

        #region ISettingsService Members

        public virtual string Get(string key, string defaultValue = null) {
            return (this.All.ContainsKey(key)
                ? this.All[key]
                : defaultValue
            );
        }


        public virtual void Set(string key, string value) {
            this.All[key] = value;
        }


        public virtual void Remove(string key) {
            if (this.All.ContainsKey(key))
                this.All.Remove(key);
        }


        public virtual void Clear() {
            this.All.Clear();
        }


        public virtual bool Contains(string key) {
            return this.All.ContainsKey(key);
        }

        #endregion
    }
}
