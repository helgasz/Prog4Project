let workers = [];
getdata();

function display() {
    workers.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            '<tr><td>' + t.workerId + '</td><td><center>' + t.workerName + '</center></td><td>' +
            `<button type="button" onclick="remove(${t.workerId})">Delete</button>` + '</td></tr>';
    });
}

async function getdata() {
    await fetch('http://localhost:20741/worker')
        .then(x => x.json())
        .then(y => {
            workers = y;
            console.log(workers);
            display();
        });
}

function remove(id) {
    alert(id);
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