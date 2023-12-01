namespace ShadowMLTTest.Assets
{
    public static class Assets
    {
        public const string shadow_bin = "AudioDataSystem.bin";

        public static byte[] shadow_bin_original() => File.ReadAllBytes("Assets\\" + shadow_bin);
    }
}