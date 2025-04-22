namespace Domain.DTOs.Others.Suppliers;

public class SupplierDto
{
    public int SupplierId { get; set; }
    public string SupplierName { get; set; }
    public List<Products> Products { get; set; }
}
