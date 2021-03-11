namespace Zip.EticketSub.Model
{
    public class StatusChk
    {
        public int Situaca { get; set; }
        public int DiasLiberados { get; set; }

    }
}


// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class Validacao
{

    private ValidacaoTable tableField;

    /// <remarks/>
    public ValidacaoTable Table
    {
        get
        {
            return this.tableField;
        }
        set
        {
            this.tableField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class ValidacaoTable
{

    private byte situacaoField;

    private byte diasLiberadosField;

    /// <remarks/>
    public byte Situacao
    {
        get
        {
            return this.situacaoField;
        }
        set
        {
            this.situacaoField = value;
        }
    }

    /// <remarks/>
    public byte DiasLiberados
    {
        get
        {
            return this.diasLiberadosField;
        }
        set
        {
            this.diasLiberadosField = value;
        }
    }
}

