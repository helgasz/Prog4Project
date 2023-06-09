﻿let workers = [];
let connection = null;
getdata();
setupSignalR();
let workerIdToUpdate = -1;

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:20741/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("WorkerCreated", (user, message) => {
        getdata();
    });

    connection.on("WorkerDeleted", (user, message) => {
        getdata();
    });

    connection.on("WorkerUpdated", (user, message) => {
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
    workers.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.workerId + "</td><td>"
        + t.workerName + "</td><td>" +
        `<button type="button" onclick="remove(${t.workerId})">Delete</button>` +
        `<button type="button" onclick="showupdate(${t.workerId})">Update</button>`
            + "</td></tr>";
    });
}

async function getdata() {
    await fetch('http://localhost:20741/worker')
        .then(x => x.json())
        .then(y => {
            workers = y;
            //console.log(workers);
            display();
        });
}

function remove(id) {
    fetch('http://localhost:20741/worker/' + id, {
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

function showupdate(id)
{   
    document.getElementById('workernametoupdate').value = workers.find(t => t['workerId'] == id)['workerName'];
    document.getElementById('updateformdiv').style.display = 'flex';
    workerIdToUpdate = id;
}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let name = document.getElementById('workernametoupdate').value;
    fetch('http://localhost:20741/worker/', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { workerName: name, workerId: workerIdToUpdate })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function create() {
    let name = document.getElementById('workername').value;
    fetch('http://localhost:20741/worker', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { workername: name })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
    
}