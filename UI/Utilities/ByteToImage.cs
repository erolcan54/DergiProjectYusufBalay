namespace UI.Utilities
{
    public static class ByteToImage
    {
        public static string DecodeImage(byte[] byteData)
        {
            string imreBase64Data = Convert.ToBase64String(byteData);
            string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
            //Passing image data in viewbag to view
            return imgDataURL;
        }

    }
}
