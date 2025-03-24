namespace Project.Common.UI
{
    public class InteractiveObjectsTextController
    {
        private readonly TextView _textView;

        public InteractiveObjectsTextController(TextView textView) =>
            _textView = textView;

        public void Draw(string objectName) =>
            _textView.DrawText($"{objectName} interact");
    }
}
