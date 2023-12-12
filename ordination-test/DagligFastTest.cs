using shared.Model;

namespace ordination_test;


[TestClass]
public class DagligFastTest
{
    
    //opret et lægemiddel
    Laegemiddel laegemiddel = new Laegemiddel("TestMiddel", 1, 1, 1, "ml");

    [TestMethod]
    //TestDoegnDosisNormal
    public void TC1()
    {

        // Opret en DagligFast ordination
        DagligFast ordination = new DagligFast(DateTime.Now, DateTime.Now.AddDays(7), laegemiddel, 2, 1, 1, 0.5);

        // Forventet daglig dosis er 2 (morgen) + 1 (middag) + 1 (aften) + 0.5 (nat) = 4.5
        double forventetDosis = 4.5;

        // Test om den beregnede daglige dosis er korrekt
        Assert.AreEqual(forventetDosis, ordination.doegnDosis());
    }
    
    
    //test metoderne nedenfor tester grænseværdierne for doegnDosis metoden
    [TestMethod]
    //TestDoegnDosisMedNulDosis
    public void TC2()
    {
        // Opret DagligFast med 0 dosis for alle doser (morgen, middag, aften, nat)
        DagligFast ordination = new DagligFast(DateTime.Now, DateTime.Now.AddDays(7), laegemiddel, 0, 0, 0, 0);
        Assert.AreEqual(0, ordination.doegnDosis(), "doegnDosis bør være 0 når alle doser er 0");
    }

    [TestMethod]
    //TestDoegnDosisMedMaksimalDosis
    public void TC3()
    {
        // Opret DagligFast med maksimal dosis for alle doser (morgen, middag, aften, nat), vi har her valgt at maksimal dosis er 500 da det ikke er opgivet i projektet
        DagligFast ordination = new DagligFast(DateTime.Now, DateTime.Now.AddDays(7), laegemiddel, 100, 100, 100, 100);
        double forventetDosis = 100 + 100 + 100 + 100; // Erstat med faktiske maksimale værdier
        Assert.AreEqual(forventetDosis, ordination.doegnDosis(), "doegnDosis bør svare til summen af maksimale doser (400)");
    }

    [TestMethod]
    //TestDoegnDosisMedEnDosis
    public void TC4()
    {
        // Opret DagligFast med dosis kun om morgenen
        DagligFast ordination = new DagligFast(DateTime.Now, DateTime.Now.AddDays(7), laegemiddel, 5, 0, 0, 0);
        Assert.AreEqual(5, ordination.doegnDosis(), "doegnDosis bør svare til morgen dosis når kun denne er givet (5)");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    //TestDoegnDosisMedNegativDosis
    public void TC5()
    {
        // Opret DagligFast med en negativ dosis
        DagligFast ordination = new DagligFast(DateTime.Now, DateTime.Now.AddDays(7), laegemiddel, -1, 0, 0, 0);
        ordination.doegnDosis(); // Forventer at denne linje smider en ArgumentException
    }

}