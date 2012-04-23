namespace BeSharpTestProject
{
    /// <summary>
    /// http://archive.cs.uu.nl/mirror/CTAN/macros/latex/contrib/oberdiek/rotchiffre.pdf
    /// http://en.literateprograms.org/Rot13_(Java)#Testing_the_Algorithm
    /// a bot of https://en.wikipedia.org/wiki/Richard_III_(play)#Synopsis from https://en.wikipedia.org/wiki/William_Shakespeare
    /// </summary>
    public class Rot13TestData : RotTestData
    {
        public Rot13TestData()
        {
            add("The quick brown fox jumps over the lazy dog", "Gur dhvpx oebja sbk whzcf bire gur ynml qbt");
            add("Now is the Winter of our Discontent,\n", "Abj vf gur Jvagre bs bhe Qvfpbagrag,\n");
            add("Made glorious Summer by this Son of Yorke:\n", "Znqr tybevbhf Fhzzre ol guvf Fba bs Lbexr:\n");
            add("And all the clouds that lowr'd vpon our house\n", "Naq nyy gur pybhqf gung ybje'q icba bhe ubhfr\n");
            add("In the deepe bosome of the Ocean buried.\n", "Va gur qrrcr obfbzr bs gur Bprna ohevrq.\n");
            add("Now are our browes bound with Victorious Wreathes,\n", "Abj ner bhe oebjrf obhaq jvgu Ivpgbevbhf Jerngurf,\n");
            add("Our bruised armes hung vp for Monuments;\n", "Bhe oehvfrq nezrf uhat ic sbe Zbahzragf;\n");
            add("Our sterne Alarums chang'd to merry Meetings;\n", "Bhe fgrear Nynehzf punat'q gb zreel Zrrgvatf;\n");
            add("Our dreadfull Marches, to delightfull Measures.\n", "Bhe qernqshyy Znepurf, gb qryvtugshyy Zrnfherf.\n");
            add("Grim-visag'd Warre, hath smooth'd his wrinkled Front:\n", "Tevz-ivfnt'q Jneer, ungu fzbbgu'q uvf jevaxyrq Sebag:\n");
            add("And now, in stead of mounting Barbed Steeds,\n", "Naq abj, va fgrnq bs zbhagvat Oneorq Fgrrqf,\n");
            add("To fright the Soules of fearfull Aduersaries,\n", "Gb sevtug gur Fbhyrf bs srneshyy Nqhrefnevrf,\n");
            add("He capers nimbly in a Ladies Chamber,\n", "Ur pncref avzoyl va n Ynqvrf Punzore,\n");
            add("To the lasciuious pleasing of a Lute.", "Gb gur ynfpvhvbhf cyrnfvat bs n Yhgr.");
        }
    }
}
