const { default: axios } = require("axios")


// const url = "http://localhost:5001/api/airport/"
// const url = "http://localhost:5218/api/airport/"
const url = "http://localhost:5000/api/airport/"

let count = 1

let stop = true;

const start = () => {
    if (stop == false)
        return
    stop = false;
    startA();
}
const startA = () => {
    setTimeout(async () => {
        if (stop) {
            return;
        }
        sendFlight()
        startA();
    }, 3000)
}

function sendFlight() {
    // console.log("flight started");
    const random = Math.random();
    if (random > 0.5) {
        get("landing")
    }
    else {
        get("departure")
    }
}

const get = (subUrl) => {
    // try {
    axios.get(`${url}${subUrl}/${count}`)
        .then(res => {
            // console.log("good", res.status, count);
            count++;
        })
        .catch(e => console.log("rejected", count))
}



const checkConnection = async () => {
    try {
        const res = await axios.get(url + "CheckConnection/")
        return true
    }
    catch (e) {
        console.log("Main Server API is Not Connected");
        return false
    }
}

const stopConnect = () => stop = true;
const reset = () => count = 1;

// return { checkConnection, start, stopConnect, reset };
// module.exports = controller

module.exports = { checkConnection, start, stopConnect, reset };