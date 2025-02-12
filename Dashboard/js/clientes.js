const apiBaseUrl = "https://localhost:44370/api/Cliente";
    

// Función asincrónica para cargar clientes
async function cargarClientesClientes() {
    try {
        console.log("Cargando clientes...");
        const response = await fetch(apiBaseUrl);

        if (!response.ok) {
            throw new Error(`HTTP error: ${response.status} ${response.statusText}`);
        }

        const data = await response.json();
        console.log("Respuesta GET:", data);

        const clientList = document.getElementById("clientList");
        clientList.innerHTML = ""; 

        if (data.exito === 1 && Array.isArray(data.data) && data.data.length > 0) {
            data.data.forEach(client => {
                const li = document.createElement("li");
                li.classList.add("list-group-item", "d-flex", "justify-content-between", "align-items-center");
                li.textContent = `ID: ${client.idCliente} - Nombre: ${client.nombre} - Correo: ${client.correo || 'No disponible'} - Teléfono: ${client.telefono || 'No disponible'} - Activo: ${client.activo ?? client.Activo}`;
                
                const btnGroup = document.createElement("div");
                btnGroup.classList.add("btn-group");
                
                const editBtn = document.createElement("button");
                editBtn.textContent = "Editar";
                editBtn.classList.add("btn", "btn-sm", "btn-outline-primary");
                editBtn.addEventListener("click", function() {
                    document.getElementById("editClientId").value = client.idCliente;
                    document.getElementById("editNombre").value = client.nombre;
                    document.getElementById("editCorreo").value = client.correo || "";
                    document.getElementById("editTelefono").value = client.telefono || "";
                    document.getElementById("editActivo").value = client.activo ?? 0;
                    document.getElementById("formEditClient").style.display = "block";
                });
                btnGroup.appendChild(editBtn);
                
                const deleteBtn = document.createElement("button");
                deleteBtn.textContent = "Eliminar";
                deleteBtn.classList.add("btn", "btn-sm", "btn-outline-danger");
                deleteBtn.addEventListener("click", function() {
                    if (confirm(`¿Estás seguro de eliminar al cliente ${client.nombre}?`)) {
                        deleteClient(client.idCliente);
                    }
                });
                btnGroup.appendChild(deleteBtn);
                
                li.appendChild(btnGroup);
                clientList.appendChild(li);
            });
        } else {
            clientList.innerHTML = "<li class='list-group-item'>No se encontraron clientes</li>";
        }
    } catch (error) {
        console.error("Error al obtener clientes:", error);
        alert("Error de conexión: " + error.message);
        document.getElementById("clientList").innerHTML = `<li class='list-group-item text-danger'>Error al cargar clientes: ${error.message}</li>`;
    }
}

// Evento para cargar clientes al presionar el botón
document.getElementById("btnLoadClients").addEventListener("click", cargarClientes);

    
    document.getElementById("formAddClient").addEventListener("submit", function(e) {
      e.preventDefault();
      
      const client = {
        Nombre: document.getElementById("nombre").value,
        Correo: document.getElementById("correo").value,
        Telefono: document.getElementById("telefono").value,
        Activo: parseInt(document.getElementById("activo").value)
      };
      
      fetch(apiBaseUrl, {
        method: "POST",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify(client)
      })
      .then(response => response.json())
      .then(data => {
        console.log("Respuesta POST:", data);
        if (data.exito === 1) {
          alert("Cliente agregado correctamente");
          document.getElementById("formAddClient").reset();
        } else {
          alert("Error al agregar cliente: " + (data.mensaje || data.Mensaje));
        }
      })
      .catch(error => {
        console.error("Error al agregar cliente:", error);
        alert("Error al agregar cliente: " + error.message);
      });
    });
    
    document.getElementById("formEditClient").addEventListener("submit", function(e) {
      e.preventDefault();
      
      const clientId = document.getElementById("editClientId").value;
      const updatedClient = {
        IdCliente: clientId,
        Nombre: document.getElementById("editNombre").value,
        Correo: document.getElementById("editCorreo").value,
        Telefono: document.getElementById("editTelefono").value,
        Activo: parseInt(document.getElementById("editActivo").value)
      };
      
      fetch(`${apiBaseUrl}`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify(updatedClient)
      })
      .then(response => response.json())
      .then(data => {
        console.log("Respuesta PUT:", data);
        if (data.exito === 1) {
          alert("Cliente actualizado correctamente");
          document.getElementById("formEditClient").reset();
          document.getElementById("formEditClient").style.display = "none";
          document.getElementById("btnLoadClients").click();
        } else {
          alert("Error al editar cliente: " + (data.mensaje || data.Mensaje));
        }
      })
      .catch(error => {
        console.error("Error al editar cliente:", error);
        alert("Error al editar cliente: " + error.message);
      });
    });
    
    // Función para eliminar un cliente (PUT para cambiar el activo a 0, no se elimina el cliente por completo)
    function deleteClient(clientId) {
      fetch(`${apiBaseUrl}/ClienteInactivo/${clientId}`, {
        method: "PUT"
      })
      .then(response => response.json())
      .then(data => {
        console.log("Respuesta DELETE:", data);
        if (data.exito === 1) {
          alert("Cliente eliminado correctamente");
          document.getElementById("btnLoadClients").click();
        } else {
          alert("Error al eliminar cliente: " + (data.mensaje || data.Mensaje));
        }
      })
      .catch(error => {
        console.error("Error al eliminar cliente:", error);
        alert("Error al eliminar cliente: " + error.message);
      });
    }