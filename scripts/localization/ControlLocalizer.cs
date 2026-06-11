namespace RSLib.GE
{
    using Godot;

    public partial class ControlLocalizer : Control
    {
        [Export] private string _key;
        [Export] private string _format;

        private bool? _plural;
        private object[] _args;

        public void SetKey(string key)
        {
            _key = key;
            Localize();
        }

        public void SetPlural(bool? plural)
        {
            _plural = plural;
            Localize();
        }
        
        public void SetPlural(int pluralCount)
        {
            _plural = pluralCount >= 2;
            Localize();
        }

        public void SetArgs(object[] args)
        {
            _args = args;
            Localize();
        }
        
        private void Localize()
        {
            string text = string.Empty;
            if (!string.IsNullOrEmpty(_key))
            {
                if (_args != null)
                {
                    text = _plural.HasValue
                           ? Localizer.FormatPluralized(_key, _plural.Value, _args)
                           : Localizer.Format(_key, _args);
                }
                else
                {
                    text = _plural.HasValue
                           ? Localizer.GetPluralized(_key, _plural.Value)
                           : Localizer.Get(_key);
                }
            }
            
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
