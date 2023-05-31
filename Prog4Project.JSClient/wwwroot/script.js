let workers = [];

fetch('http://localhost:20741/worker')
    .then(x => x.json())
    .then(y => {
        workers = y;
        console.log(workers);
        display();
    });


function display() {
    workers.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.workerId + "</td><td>" + t.workerName + "</td></tr>";
    });
}