namespace RSLib.GE
{
    using Godot;

    public partial class ControlLocalizer : Control
    {
        [Export] private string _key;
        [Export] private string _format;

        private object[] _args;

        public void SetKey(string key)
        {
            _key = key;
            Localize();
        }

        public void SetArgs(object[] args)
        {
            _args = args;
            Localize();
        }
        
        private void Localize()
        {
            string text = _args != null
                ? Localizer.Format(_key, _args)
                : Localizer.Get(_key);
            
            if (!string.IsNullOrEmpty(_format))
                text = string.Format(_format, text);
            
            switch (GetParent())
            {
                case Label label:
                    label.Text = text;
                    break;
                case RichTextLabel richTextLabel:
                    richTextLabel.Text = text;
                    break;
                case Button button:
                    button.Text = text;
                    break;
            }
        }

        public override void _EnterTree()
        {
            base._EnterTree();
            
            Localizer.LanguageChanged += Localize;
            Localize();
        }

        public override void _ExitTree()
        {
            base._ExitTree();
            Localizer.LanguageChanged -= Localize;
        }

        public override void _Ready()
        {
            base._Ready();
            
            Size = Vector2.Zero;
            MouseFilter = MouseFilterEnum.Ignore;
        }
    }
}
