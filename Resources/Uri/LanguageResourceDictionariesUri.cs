namespace Black_Hole.Resources.Uri
{
    internal struct LanguageResourceDictionariesUri
    {
        private static readonly System.Uri BaseUri = new("pack://application:,,,/Black Hole;component/Resources/Languages/");

        public static readonly System.Uri EnglishUsLanguageUri = new(BaseUri + "lang.en-US.xaml");
    }
}
