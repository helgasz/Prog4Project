let positions = [];
let connection = null;
getdata();
setupSignalR();
let positionIdtoUpdate = -1;

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:20741/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("PositionCreated", (user, message) => {
        getdata();
    });

    connection.on("PositionDeleted", (user, message) => {
        getdata();
    });

    connection.on("PositionUpdated", (user, message) => {
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
    positions.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
        "<tr><td>" + t.positionId + "</td><td>"
        + t.projectId + "</td><td>"  
        + t.workerId + "</td><td>" 
        + t.priority + "</td><td>"
        + t.positionName + "</td><td>"
        +`<button type="button" onclick="remove(${t.positionId})">Delete</button>` +
        `<button type="button" onclick="showupdate(${t.positionId})">Update</button>`
            + "</td></tr>";
    });
}

async function getdata() {
    await fetch('http://localhost:20741/position')
        .then(x => x.json())
        .then(y => {
            positions = y;

            display();
        });
}

function remove(id) {
    fetch('http://localhost:20741/position/' + id, {
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
    document.getElementById('positionnametoupdate').value = positions.find(t => t['positionId'] == id)['positionName'];
    document.getElementById('workertoupdate').value = positions.find(t => t['positionId'] == id)['workerId'];
    document.getElementById('prioritytoupdate').value = positions.find(t => t['positionId'] == id)['priority'];
    document.getElementById('updateformdiv').style.display = 'flex';
    positionIdtoUpdate = id;
}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let name = document.getElementById('positionnametoupdate').value;
    let worker = document.getElementById('workertoupdate').value;
    let prio = document.getElementById('prioritytoupdate').value;
    fetch('http://localhost:20741/position/', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { positionName: name, positionId: positionIdtoUpdate, workerId: worker, priority: prio })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function create() {
    let name = document.getElementById('positionNew').value;
    let project = document.getElementById('projectIDnew').value;
    let worker = document.getElementById('workerIDnew').value;
    let prio = document.getElementById('priorityNew').value;
    fetch('http://localhost:20741/position', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { positionName: name, projectId: project, workerId: worker, priority: prio })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}