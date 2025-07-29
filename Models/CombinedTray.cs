public class CombinedTray
{
    public int tray_id { get; set; }
    public double temperature { get; set; }
    public double humidity { get; set; }
    public double light { get; set; }
    public string plant_type { get; set; } = string.Empty;
    public double target_temperature { get; set; }
    public double target_humidity { get; set; }
    public double target_light { get; set; }
    public bool is_out_of_range { get; set; }
}