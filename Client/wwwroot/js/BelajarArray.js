//declaring Array 1D

let array = [1, 2, 3, 4];
//array multidimensional
let arrayMultiDimensional = ['a', 'b', 'c', [1, 2, 'e'], true];
console.log(array[2]);
console.log(arrayMultiDimensional[3][2]);

let element = null;
for (var i = 0; i < array.length; i++) {
    element = array[i];
}

console.log(array);

array.push('hai');
array.push('haloo');
console.log(array);

array.pop();
console.log(array);

array.unshift('inis dideoan');
console.log(array);
array.shift();
console.log(array);

let mahasiswa = {
    name: "jonathan",
    nim: 'a112016101010',
    umur: 24,
    hobby: ['maingame', 'wibu', 'renang'],
    inActive: true
}

const user = {};
user.nama = "budi";
user.umur = '20';
user.nim = 'as231324'

console.log(mahasiswa);
console.log(user);

let key = 'umur';
console.log(user[key]);


//lambda function/arrow function
const hitung1 = (num1, num2) => {
    const jumlah = num1 + num2;
    return jumlah;
}
console.log(hitung1(5, 10));


//sama aja 
const hitung2 = (num1, num2) => num1 + num2;
console.log(hitung2(2, 3));



const animals = [
    { name: 'Nemo', species: 'Fish', class: {name : 'Invertebrata'}},
    { name: 'Simba', species: 'Cat', class: {name : 'Mamalia'}},
    { name: 'Dory', species: 'Fish', class: { name: 'Invertebrata'}},
    { name: 'Panther', species: 'Cat', class: {name : 'Mamalia'}},
    { name: 'Budi', species: 'Cat', class: {name : 'Mamalia'}}

]


/*Kode Utama*/
const kucing = [];
const ikan = [];
for (var i = 0; i < animals.length; i++) {
    if (animals[i].species == 'Fish') {
        animals[i].class.name = 'Non-Mamalia'            
    }
    else if (animals[i].species == 'Cat') {
        const objectkucing = {};
        objectkucing.name = animals[i].name,
        objectkucing.species = animals[i].species,
        objectkucing.class = { name: animals[i].class.name },
        kucing.push(objectkucing)
    };
};

/*Tugas 1*/
console.log(animals);
/*Tugas 2*/
console.log(kucing);

var kucingaja = animals.filter(animal =>animal.species === 'Cat')


