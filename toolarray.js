console.log("Starting...")
const readline = require('readline');

var users = [];

class ToolRepository {
    userTools = new Map();
    constructor(userTools) {
        this.userTools = userTools;
    }
    getTools = (user) => {
        return this.userTools.get(user);
    }
}

class MyClass {
    myToolRepository;
    user;
    userTools = [];
    constructor(myToolRepository, user) {
        this.myToolRepository = myToolRepository;
        this.user = user;
        this.userTools = myToolRepository.getTools(user)
    }

    printMyArray = () => {
        let a = "";
        for (let i = 0; i < this.userTools.length; i++) {
            a += this.userTools[i] + ",";
        }
        let lastComma = a.lastIndexOf(",");
        a = a.substring(0, lastComma);
        return a;
    }
    getMyRandomTool = () => {
        var randomPosition = Math.floor(Math.random() * this.userTools.length);
        return this.userTools[randomPosition];
    }
};

//var user1 = "Retro";
//var user2 = "Greed";



//var userTools = new Map();
//userTools.set(user1, ["foo", "bar", "wrench", "socket", "hammer"]);
//userTools.set(user2, ["knife", "sandwich", "wrench", "socket", "hammer"]);

//var userToolRepo = new ToolRepository(userTools);

//var myClassInstance = new MyClass(userToolRepo, user1);
/*
console.log("User: " + myClassInstance.user);
console.log("My array values: " + myClassInstance.printMyArray());
for (let i = 0; i < 6; i++) {
    console.log("Random Tool: " + myClassInstance.getMyRandomTool());
}

var myClassInstance1 = new MyClass(userToolRepo, user2);
console.log("User: " + myClassInstance1.user);
console.log("My array values: " + myClassInstance1.printMyArray());
for (let i = 0; i < 6; i++) {
    console.log("Random Tool: " + myClassInstance1.getMyRandomTool());
}*/
var intUserNum;

const rl = readline.createInterface({
    input: process.stdin,
    output: process.stdout
});

rl.on('close', function () {
    console.log("\n Closing");
    process.exit(0)
});

rl.question("how many users? ", function (numUsers) {
    console.log(numUsers);
    intUserNum = parseInt(numUsers);
    users = new Array(intUserNum);


    var getUserName = function (currentUser, callback) {
        intCurrentUser = parseInt(currentUser);
        rl.question("what is the user " + (currentUser + 1) + " name?", function (name) {
            users[intCurrentUser] = name;
            next = intCurrentUser + 1;
            if (next < intUserNum) {
                getUserName(next , callback);
            } else {
                callback();
            }

        });
    }

    getUserName(0, () => {
        let all;
        users.forEach(element => {
            all += element + " ";
        });
        console.log("Users: "+ all)

        rl.close();

    });

});


