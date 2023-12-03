using ShadowMLT;
namespace ShadowMLTTest
{
    public class Parsing
    {
        private const string parentDirectory = "Assets\\";

        [Fact]
        public void ReadShadowBin_Success()
        {
            var shadowBinPath = parentDirectory + Assets.Assets.shadow_bin;
            var shadowMltPath = parentDirectory + Assets.Assets.shadow_mlt;
            var gcax = GCAX.ParseMLTandBIN(shadowMltPath, shadowBinPath);
/*            Assert.Equal(fileName, fnt.fileName);
            Assert.Equal("", fnt.filterString);
            Assert.Equal(471, fnt.GetEntryTableCount());


            int expectedDataIndex = 120;
            Assert.Equal(23478, fnt.GetEntrySubtitleAddress(expectedDataIndex));
            Assert.Equal(64100, fnt.GetEntryMessageIdBranchSequence(expectedDataIndex));
            Assert.Equal(EntryType.TRIGGER_OBJECT, fnt.GetEntryEntryType(expectedDataIndex));
            Assert.Equal(172, fnt.GetEntrySubtitleActiveTime(expectedDataIndex));
            Assert.Equal(1564, fnt.GetEntryAudioId(expectedDataIndex));
            Assert.Equal("This cage is protected by\nthose GUN soldiers.\0", fnt.GetEntrySubtitle(expectedDataIndex));*/
        }
    }
}
