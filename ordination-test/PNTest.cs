using shared.Model;

namespace ordination_test;

public class PNTest
{
    //opret et lægemiddel
    Laegemiddel laegemiddel = new Laegemiddel("TestMiddel", 1, 1, 1, "ml");
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestPNGivDosisMedNullParameter()
    {
        
        //opret en PN ordination
        PN ordination = new PN(DateTime.Now, DateTime.Now.AddDays(7), 2, laegemiddel);
            
        //Forsøger at anvende dosis på en dato, men sender en null parameter med istedet for en dato    
        ordination.givDosis(null); // Dette kald forventes at smide en ArgumentNullException
    }
}