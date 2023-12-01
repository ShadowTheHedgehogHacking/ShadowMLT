using ShadowMLT;
namespace ShadowMLTTest
{
    public class Parsing
    {
        private const string parentDirectory = "Assets\\";

        [Fact]
        public void ReadShadowBin_Success()
        {
            var fileName = parentDirectory + Assets.Assets.shadow_bin;
            var gcax = GCAX.ParseMLTandBIN("temp", fileName);
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
