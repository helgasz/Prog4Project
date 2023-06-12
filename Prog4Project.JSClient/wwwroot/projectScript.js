let projects = [];
let connection = null;
getdata();
setupSignalR();
let projectIDtoUpdate = -1;


function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:20741/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("ProjectCreated", (user, message) => {
        getdata();
    });

    connection.on("ProjectDeleted", (user, message) => {
        getdata();
    });

    connection.on("ProjectUpdated", (user, message) => {
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
    projects.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.projectId + "</td><td>"
            + t.projectName + "</td><td>"
            + t.price + "</td><td>"
            + t.managerId + "</td><td>"
            + t.difficulity + "</td><td>"
            + `<button type="button" onclick="remove(${t.projectId})">Delete</button>` +
            `<button type="button" onclick="showupdate(${t.projectId})">Update</button>`
            + "</td></tr>";
    });
}

async function getdata() {
    await fetch('http://localhost:20741/project')
        .then(x => x.json())
        .then(y => {
            projects = y;

            display();
        });
}

function remove(id) {
    fetch('http://localhost:20741/project/' + id, {
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
    document.getElementById('projectNameUpdate').value = projects.find(t => t['projectId'] == id)['projectName'];
    document.getElementById('priceUpdate').value = projects.find(t => t['projectId'] == id)['price'];
    document.getElementById('managerUpdate').value = projects.find(t => t['projectId'] == id)['managerId'];
    document.getElementById('diffUpdate').value = projects.find(t => t['projectId'] == id)['difficulity'];
    document.getElementById('updateformdiv').style.display = 'flex';
    projectIDtoUpdate = id;
}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let name = document.getElementById('projectNameUpdate').value;
    let newprice = document.getElementById('priceUpdate').value;
    let newmanager = document.getElementById('managerUpdate').value;
    let diff = document.getElementById('diffUpdate').value;
    fetch('http://localhost:20741/project/', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { projectName: name, projectId: projectIDtoUpdate, price: newprice, managerId: newmanager, difficulity: diff })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function create() {
    let pro = document.getElementById('projectNameNew').value;
    let pri = document.getElementById('projectPriceNew').value;
    let man = document.getElementById('projectManagerIDNew').value;
    let diff = document.getElementById('projectDiffNew').value;
    fetch('http://localhost:20741/project', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { projectName: pro, price: pri, managerId: man, difficulity: diff })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function calculateAverageDifficulty(managerId, projects) {
    
    const managerProjects = projects.filter(project => parseInt(project.managerId) === parseInt(managerId));
    console.log("Manager Projects:", managerProjects);
    console.log("Total Projects:", managerProjects.length);

    if (managerProjects.length === 0) {
        return 0;
    }

    const totalDifficulty = managerProjects.reduce((sum, project) => sum + project.difficulity, 0);
    console.log("Total Difficulty:", totalDifficulty);

    return totalDifficulty / managerProjects.length;
}



function countProjectsByManager(managerId, projects) {
    let projectCount = 0;
    projects.forEach(project => {        
        if (parseInt(project.managerId) === parseInt(managerId)) {
            projectCount++;
        }
    });

    return projectCount;
}

function giveInfo() {
    const managerId = document.getElementById('ManagerIdInfo').value;
    const projectCount = countProjectsByManager(managerId, projects);

    document.getElementById('avarageDifMan').textContent = "Average Difficulty for Manager's Projects: " + calculateAverageDifficulty(managerId, projects);
    document.getElementById('projectNumero').textContent = "Number of Manager's Projects: " + projectCount;
}