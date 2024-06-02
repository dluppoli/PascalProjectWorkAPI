# Project Work Pascal 2024

## Documentazione API

### Categorie
Request:
```http
GET /categories?search={search}&page={page}&pagesize={pagesize}
```
Parametri:
| Parametro | Descrizione | Obbligatorio |
| :--- | :--- | :--- |
| `{search}` | Termine di ricerca | |
| `{page}` | Numero di pagina da restituire (default 1) | |
| `{search}` | Numero di risultati per ogni pagina (default 10) | |

Response:
```json
{
    "result": [
        {
            "id": 1,
            "name": "Abbigliamento",
            "image": "QkIa5tT.jpeg"
        },
        {
            "id": 2,
            "name": "Elettronica",
            "image": "ZANVnHE.jpeg"
        }
    ],
    "totalRecordsCount": 2
}
```

### Categoria
Request:
```http
GET /categories/{id}
```
Parametri:
| Parametro | Descrizione | Obbligatorio |
| :--- | :--- | :--- |
| `{id}` | Codice categoria | Si |

Response:
```json
{
    "id": 1,
    "name": "Abbigliamento",
    "image": "QkIa5tT.jpeg"
}
```

### Prodotti
Request:
```http
GET /products?search={search}&page={page}&pagesize={pagesize}
```
Parametri:
| Parametro | Descrizione | Obbligatorio |
| :--- | :--- | :--- |
| `{search}` | Termine di ricerca | |
| `{page}` | Numero di pagina da restituire (default 1) | |
| `{search}` | Numero di risultati per ogni pagina (default 10) | |

Response:
```json
{
    "result": [
         {
            "id": 1,
            "title": "T-Shirt Grafica Montagna Maestosa",
            "description": "Eleva il tuo guardaroba con questa elegante t-shirt nera caratterizzata da una sorprendente grafica monocromatica di una catena montuosa. Perfetta per chi ama la natura o desidera aggiungere un tocco di design ispirato alla natura al proprio look, questa maglietta è realizzata in tessuto morbido e traspirante che garantisce comfort per tutto il giorno. Ideale per uscite casual o come regalo unico, questa t-shirt è un'aggiunta versatile a qualsiasi collezione.",
            "price": 44,
            "stars": 4.5,
            "images": "QkIa5tT.jpeg;jb5Yu0h.jpeg;UlxxXyG.jpeg",
            "idCategory": 1,
            "category": {
                "id": 1,
                "name": "Abbigliamento",
                "image": "QkIa5tT.jpeg"
            }
        }
    ],
    "totalRecordsCount": 1
}
```

### Prodotto
Request:
```http
GET /products/{id}?search={search}&page={page}&pagesize={pagesize}
```
Parametri:
| Parametro | Descrizione | Obbligatorio |
| :--- | :--- | :--- |
| `{id}` | Codice categoria | Si |
| `{search}` | Termine di ricerca | |
| `{page}` | Numero di pagina da restituire (default 1) | |
| `{search}` | Numero di risultati per ogni pagina (default 10) | |

Response:
```json
{
    "id": 1,
    "title": "T-Shirt Grafica Montagna Maestosa",
    "description": "Eleva il tuo guardaroba con questa elegante t-shirt nera caratterizzata da una sorprendente grafica monocromatica di una catena montuosa. Perfetta per chi ama la natura o desidera aggiungere un tocco di design ispirato alla natura al proprio look, questa maglietta è realizzata in tessuto morbido e traspirante che garantisce comfort per tutto il giorno. Ideale per uscite casual o come regalo unico, questa t-shirt è un'aggiunta versatile a qualsiasi collezione.",
    "price": 44,
    "stars": 4.5,
    "images": "QkIa5tT.jpeg;jb5Yu0h.jpeg;UlxxXyG.jpeg",
    "idCategory": 1,
    "category": {
        "id": 1,
        "name": "Abbigliamento",
        "image": "QkIa5tT.jpeg"
    }
}
```

### Prodotti di una categoria
Request:
```http
GET /categories/{id}/products?search={search}&page={page}&pagesize={pagesize}
```
Parametri:
| Parametro | Descrizione | Obbligatorio |
| :--- | :--- | :--- |
| `{id}` | Codice categoria | Si |
| `{search}` | Termine di ricerca | |
| `{page}` | Numero di pagina da restituire (default 1) | |
| `{search}` | Numero di risultati per ogni pagina (default 10) | |

Response:
```json
{
    "result": [
         {
            "id": 1,
            "title": "T-Shirt Grafica Montagna Maestosa",
            "description": "Eleva il tuo guardaroba con questa elegante t-shirt nera caratterizzata da una sorprendente grafica monocromatica di una catena montuosa. Perfetta per chi ama la natura o desidera aggiungere un tocco di design ispirato alla natura al proprio look, questa maglietta è realizzata in tessuto morbido e traspirante che garantisce comfort per tutto il giorno. Ideale per uscite casual o come regalo unico, questa t-shirt è un'aggiunta versatile a qualsiasi collezione.",
            "price": 44,
            "stars": 4.5,
            "images": "QkIa5tT.jpeg;jb5Yu0h.jpeg;UlxxXyG.jpeg",
            "idCategory": 1,
            "category": {
                "id": 1,
                "name": "Abbigliamento",
                "image": "QkIa5tT.jpeg"
            }
        }
    ],
    "totalRecordsCount": 1
}
```

### Nuovo Ordine
Request:
```http
POST /orders
```

Request Body:
```json
{
  "clientName": "Marco Rossi",
  "address": "Via di qua 123",
  "totalPrice": 44,
  "payment": {
    "number": "1234567890123456",
    "ownerName": "Marco Rossi",
    "expire": "0724",
    "cvv": 123
  },
  "details": [
    {
      "idProduct": 1,
      "quantity": 1
    }
  ]
}
```