namespace shared.Model;

public class PN : Ordination {
	public double antalEnheder { get; set; }
    public List<Dato> dates { get; set; } = new List<Dato>();

    public PN (DateTime startDen, DateTime slutDen, double antalEnheder, Laegemiddel laegemiddel) : base(laegemiddel, startDen, slutDen) {
		this.antalEnheder = antalEnheder;
	}

    public PN() : base(null!, new DateTime(), new DateTime()) {
    }

    /// <summary>
    /// Registrerer at der er givet en dosis på dagen givesDen
    /// Returnerer true hvis givesDen er inden for ordinationens gyldighedsperiode og datoen huskes
    /// Returner false ellers og datoen givesDen ignoreres
    /// </summary>
    public bool givDosis(Dato givesDen)
    {
        if (givesDen == null)
        {
            throw new ArgumentNullException(nameof(givesDen), "givesDen kan ikke være null");
        }

        if (givesDen.dato >= startDen && givesDen.dato <= slutDen)
        {
            dates.Add(givesDen);
            return true;
        }
        return false;
    }


    public override double doegnDosis()
    {
        //bør istedet kigge i dates listen og se om ordinations id'et er der for at afgøre om ordinationen er givet
        int gangeGivet = dates.Count();

        // Antal dage mellem første og sidste givning
        int antalDage = (int)(slutDen - startDen).TotalDays + 1;

        // Sikre, at der er nok dage til at foretage en beregning
        if (antalDage < 1 || gangeGivet < 1)
            return -1; // Returner 0, hvis der ikke er nok dage eller ordinationen ikke er anvendt
        
        // Beregning af døgndosis
        return (gangeGivet * antalEnheder) / antalDage;
    }



    public override double samletDosis() {
        return dates.Count() * antalEnheder;
    }

    public int getAntalGangeGivet() {
        return dates.Count();
    }

	public override String getType() {
		return "PN";
	}
}
