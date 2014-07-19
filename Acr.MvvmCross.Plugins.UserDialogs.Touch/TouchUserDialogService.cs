using System;
using System.Linq;
using BigTed;
using MonoTouch.UIKit;


namespace Acr.MvvmCross.Plugins.UserDialogs.Touch {
    
    public class TouchUserDialogService : AbstractUserDialogService {

        public override void ActionSheet(ActionSheetConfig config) {
            this.Dispatch(() =>  {
                var action = new UIActionSheet(config.Title);
                config.Options.ToList().ForEach(x => action.AddButton(x.Text));

                action.Clicked += (sender, btn) => config.Options[btn.ButtonIndex].Action();
                var view = Utils.GetTopView();
                action.ShowInView(view);
            });
        }


        public override void Alert(AlertConfig config) {
            this.Dispatch(() =>  {
                var dlg = new UIAlertView(config.Title ?? String.Empty, config.Message, null, null, config.OkText);
                if (config.OnOk != null) 
                    dlg.Clicked += (s, e) => config.OnOk();
                
                dlg.Show();
            });
        }


        public override void Confirm(ConfirmConfig config) {
            this.Dispatch(() =>  {
                var dlg = new UIAlertView(config.Title ?? String.Empty, config.Message, null, config.CancelText, config.OkText);
                dlg.Clicked += (s, e) => {
                    var ok = (dlg.CancelButtonIndex != e.ButtonIndex);
                    config.OnConfirm(ok);
                };
                dlg.Show();
            });
        }


        public override void Toast(string message, int timeoutSeconds, Action onClick) {
            // TODO: no click callback in showtoast at the moment
            this.Dispatch(() =>  {
                var ms = timeoutSeconds * 1000;
                BTProgressHUD.ShowToast(message, false, ms);
            });
            
        }


        public override void Prompt(PromptConfig config) {
            this.Dispatch(() =>  {
                var result = new PromptResult();
                var dlg = new UIAlertView(config.Title ?? String.Empty, config.Message, null, config.CancelText, config.OkText) {
                    AlertViewStyle = config.IsSecure
                        ? UIAlertViewStyle.SecureTextInput 
                        : UIAlertViewStyle.PlainTextInput
                };
                var txt = dlg.GetTextField(0);
                txt.SecureTextEntry = config.IsSecure;
                txt.Placeholder = config.Placeholder;

                //UITextView = editable
                dlg.Clicked += (s, e) => {
                    result.Ok = (dlg.CancelButtonIndex != e.ButtonIndex);
                    result.Text = txt.Text;
                    config.OnResult(result);
                };
                dlg.Show();
            });
        }


        protected override IProgressDialog CreateDialogInstance() {
            return new TouchProgressDialog();
        }


        protected virtual void Dispatch(Action action) {
            UIApplication.SharedApplication.InvokeOnMainThread(() => action());
        }
    }
}