namespace Black_Hole.Resources.Uri
{
    internal struct StyleResourceDictionariesUri
    {
        private static readonly System.Uri BaseUri = new("pack://application:,,,/Black_Hole;component/Resources/Styles/");

        public static readonly System.Uri ButtonStyleUri = new(BaseUri + "Button.xaml");

        public static readonly System.Uri TextBlockStyleUri = new(BaseUri + "TextBlock.xaml");

        public static readonly System.Uri LabelStyleUri = new(BaseUri + "Label.xaml");
    }
}
