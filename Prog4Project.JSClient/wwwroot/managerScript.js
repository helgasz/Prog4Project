let managers = [];
let connection = null;
getdata();
setupSignalR();
let managerIdtoUpdate = -1;

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:20741/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("ManagerCreated", (user, message) => {
        getdata();
    });

    connection.on("ManagerDeleted", (user, message) => {
        getdata();
    });

    connection.on("ManagerUpdated", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};



function display() {
    document.getElementById('resultarea').innerHTML = "";
    managers.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.managerId + "</td><td>"
            + t.managerName + "</td><td>" +
            `<button type="button" onclick="remove(${t.managerId})">Delete</button>` +
            `<button type="button" onclick="showupdate(${t.managerId})">Update</button>`
            + "</td></tr>";
    });
}

async function getdata() {
    await fetch('http://localhost:20741/manager')
        .then(x => x.json())
        .then(y => {
            managers = y;
            
            display();
        });
}

function remove(id) {
    fetch('http://localhost:20741/manager/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function showupdate(id) {
    document.getElementById('managernametoupdate').value = managers.find(t => t['managerId'] == id)['managerName'];
    document.getElementById('updateformdiv').style.display = 'flex';
    managerIdtoUpdate = id;
}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let name = document.getElementById('managernametoupdate').value;
    fetch('http://localhost:20741/manager/', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { managerName: name, managerId: managerIdtoUpdate })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function create() {
    let name = document.getElementById('managername').value;
    fetch('http://localhost:20741/manager', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { managerName: name })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}