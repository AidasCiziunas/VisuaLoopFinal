using SyedAli.Main.UIModule.Widgets;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SyedAli.Main.UIModule.MenuPanel
{
    public class MenuPopUpWindowController : AWindowController<MenuPopUpWindowProperties>
    {
        [SerializeField] Transform _thisT;
        [Space(10)]

        [SerializeField] TextMeshProUGUI _mainTxt;
        [SerializeField] CrossButtonWidgetController _crossBtnWidCtrl;

        [Space(10)]
        [SerializeField] SimpleButtonWidgetController _btnWidCtrl1;
        [SerializeField] TelephoneButtonWidgetController _telephoneWidCtrl2;
        [SerializeField] EmailButtonWidgetController _emailWidCtrl3;
        [SerializeField] WebsiteButtonWidgetController _websiteBtnWidCtrl;
        [SerializeField] LinkedinButtonWidgetController _linkedinBtnWidCtrl;
        [SerializeField] FacebookButtonWidgetController _facebookBtnWidCtrl;
        [SerializeField] InstagramButtonWidgetController _instagramBtnWidCtrl;

        protected override void AddListeners()
        {
            _crossBtnWidCtrl.MainBtnPointerClicked_EventHandler += OnClickCross;
            _websiteBtnWidCtrl.MainBtnPointerClicked_EventHandler += OnClickWebsiteBtn;
            _emailWidCtrl3.MainBtnPointerClicked_EventHandler += OnClickEmailBtn;
            _linkedinBtnWidCtrl.MainBtnPointerClicked_EventHandler += OnClickLinkedinBtn;
            _facebookBtnWidCtrl.MainBtnPointerClicked_EventHandler += OnClickFacebookBtn;
            _instagramBtnWidCtrl.MainBtnPointerClicked_EventHandler += OnClickInstagramBtn;
        }

        protected override void RemoveListeners()
        {
            _crossBtnWidCtrl.MainBtnPointerClicked_EventHandler -= OnClickCross;
            _websiteBtnWidCtrl.MainBtnPointerClicked_EventHandler -= OnClickWebsiteBtn;
            _emailWidCtrl3.MainBtnPointerClicked_EventHandler -= OnClickEmailBtn;
            _linkedinBtnWidCtrl.MainBtnPointerClicked_EventHandler -= OnClickLinkedinBtn;
            _facebookBtnWidCtrl.MainBtnPointerClicked_EventHandler -= OnClickFacebookBtn;
            _instagramBtnWidCtrl.MainBtnPointerClicked_EventHandler -= OnClickInstagramBtn;
        }

        protected override void OnPropertiesSet()
        {
            base.OnPropertiesSet();

            TimeSpan afternoonTimeSpan = new TimeSpan(12, 00, 00);
            TimeSpan eveningTimeSpan = new TimeSpan(18, 00, 00);

            if (DateTime.Now.TimeOfDay < afternoonTimeSpan)
                _mainTxt.text = LanguageModuleSignals.Get<LanguageSignal.GetRelevantLangSent>().Dispatch(SentenceToken.GoodMorning);
            else if (DateTime.Now.TimeOfDay > afternoonTimeSpan && DateTime.Now.TimeOfDay < eveningTimeSpan)
                _mainTxt.text = LanguageModuleSignals.Get<LanguageSignal.GetRelevantLangSent>().Dispatch(SentenceToken.GoodAfternoon);
            else
                _mainTxt.text = LanguageModuleSignals.Get<LanguageSignal.GetRelevantLangSent>().Dispatch(SentenceToken.GoodEvening);

            _btnWidCtrl1.SetData(new SimpleButtonWidgetProperties(false, LanguageModuleSignals.Get<LanguageSignal.GetRelevantLangSent>().Dispatch(SentenceToken.Products)));
            _telephoneWidCtrl2.SetData(new TelephoneButtonWidgetProperties(false, "18778743539"));
            _emailWidCtrl3.SetData(new EmailButtonWidgetProperties(true, "sales@flexhose.com"));
            _websiteBtnWidCtrl.SetData(new WebsiteButtonWidgetProperties("https://flexhose.com/"));
            _linkedinBtnWidCtrl.SetData(new LinkedinButtonWidgetProperties("https://www.linkedin.com/company/flex-hose-co.-inc./"));
            _facebookBtnWidCtrl.SetData(new FacebookButtonWidgetProperties("https://www.facebook.com/profile.php?id=100085774458587"));
            _instagramBtnWidCtrl.SetData(new InstagramButtonWidgetProperties("https://www.instagram.com/flexhoseco/?igshid=YmMyMTA2M2Y%3D"));

            Properties.TopBarAct?.Invoke(_thisT);
        }

        protected override void WhileHiding()
        {
            base.WhileHiding();
        }

        private void OnClickCross(object sender, EventArgs e)
        {
            Properties.CloseAct?.Invoke();
            UI_Close();
        }

        private void OnClickWebsiteBtn(object sender, WebsiteBtnClickedEventArgs e)
        {
            Application.OpenURL(e.URL);
        }

        private void OnClickEmailBtn(object sender, EventArgs e)
        {
            SendEmail();
        }

        private void OnClickLinkedinBtn(object sender, LinkedinBtnClickedEventArgs e)
        {
            Application.OpenURL(e.URL);
        }

        private void OnClickFacebookBtn(object sender, FacebookBtnClickedEventArgs e)
        {
            Application.OpenURL(e.URL);
        }

        private void OnClickInstagramBtn(object sender, InstagramBtnClickedEventArgs e)
        {
            Application.OpenURL(e.URL);
        }
        void SendEmail()
        {
            string email = _emailWidCtrl3.Props.MainText;
            string subject = MyEscapeURL("");
            string body = MyEscapeURL("");
            Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
        }
        string MyEscapeURL(string url)
        {
            return WWW.EscapeURL(url).Replace("+", "%20");
        }
    }
}
