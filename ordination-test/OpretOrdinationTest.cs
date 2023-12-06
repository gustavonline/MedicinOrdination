namespace ordination_test;

using Microsoft.EntityFrameworkCore;

using Service;
using Data;
using shared.Model;

[TestClass]
public class OpretOrdinationTest
{
    private DataService service;

    [TestInitialize]
    public void SetupBeforeEachTest()
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrdinationContext>();
        optionsBuilder.UseInMemoryDatabase(databaseName: "test-database");
        var context = new OrdinationContext(optionsBuilder.Options);
        service = new DataService(context);
        service.SeedData();
    }

    [TestMethod]
    public void PatientsExist()
    {
        Assert.IsNotNull(service.GetPatienter());
    }

    [TestMethod]
    public void OpretDagligFast()
    {
        
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        Assert.AreEqual(1, service.GetDagligFaste().Count());

        service.OpretDagligFast(patient.PatientId, lm.LaegemiddelId,
            2, 2, 1, 0, DateTime.Now, DateTime.Now.AddDays(3));

        Assert.AreEqual(2, service.GetDagligFaste().Count());
    }
    
    [TestMethod]
    public void TestOmDagligFastPatientOgLaegemiddelIkkeFindes()
    {
        int ikkeEksisterendePatientId = -1;
        int ikkeEksisterendeLaegemiddelId = -1;

        Laegemiddel lm = service.GetLaegemidler().First();

        Assert.ThrowsException<ArgumentNullException>(() =>
            service.OpretDagligFast(ikkeEksisterendePatientId, lm.LaegemiddelId, 2, 2, 1, 0, DateTime.Now, DateTime.Now.AddDays(3)));

        
    }
    
    //tester exceptionel flow i usecase "opret ordinatioN"
    [TestMethod]
    public void TestStartDatoEfterSlutDatoException()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        DateTime startDato = DateTime.Now.AddDays(3);
        DateTime slutDato = DateTime.Now; // Startdato er efter slutdato

        Assert.ThrowsException<ArgumentNullException>(() =>
            service.OpretDagligFast(patient.PatientId, lm.LaegemiddelId, 2, 2, 1, 0, startDato, slutDato));
    }
    
}