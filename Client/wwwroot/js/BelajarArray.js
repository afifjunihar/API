console.log("test");

//objek

let mahasiswa = {
    nama: "Udin",
    umur: 24,
    hobi: ['Wibu', 'Musik', 'Tidur']
}

console.log(mahasiswa);

//lambda

const hitung = (num1, num2) => num1 + num2;
const hitung2 = (num1, num2) => {
    const jumlah = num1 + num2;
    return jumlah
}

console.log(hitung(5, 10));
console.log(hitung2(5, 10));

//array of object
const animals = [
    { name: 'Nemo', species: 'fish', class: { name: 'invertebrata' } },
    { name: 'Simba', species: 'Cat', class: { name: 'Mamalia' } },
    { name: 'Dory', species: 'fish', class: { name: 'invertebrata' } },
    { name: 'Panther', species: 'Cat', class: { name: 'Mamalia' } },
    { name: 'Budi', species: 'Cat', class: { name: 'Mamalia' } },
    { name: 'Binomo', species: 'Cat', class: {name: 'Mamalia'} }
]

//tugas bikin kallo fish jadinya non mamalia
for (var i = 0; i < animals.length; i++) {
    if (animals[i].species == 'fish') {
        animals[i].class.name = 'non-Mamalia'
    }
}

console.log(animals);

const onlyCat = []
for (var i = 0; i < animals.length; i++) {
    if (animals[i].species == 'Cat') {
        onlyCat.push(animals[i]);
    }
}

console.log(onlyCat);
