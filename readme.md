### Run the project

```bash
dotnet run
```

### Test the project

```bash
dotnet test
```

The API will be available at:  
`http://localhost:5000/plant-sensor-data`

Swagger UI:  
`http://localhost:5000/swagger`


---

## 🛠️ How It Works

- `GET /plant-sensor-data`  
  - Fetches sensor data and plant configurations from:
    - `http://3.0.148.231:8010/sensor-readings`
    - `http://3.0.148.231:8020/plant-configurations`
  - Combines them using `tray_id`
  - Highlights data where actual values are **below** target thresholds
  - Saves the result to: `Data/plant_data.json`
  - Returns combined result in HTTP response

---