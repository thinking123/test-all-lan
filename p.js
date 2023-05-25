const majorDivisors = [2.0, 2.5, 2.0]

let majorSkip = 1
let majorSkipPower = 0
let majorDivisionPixels = 0.1

while (majorDivisionPixels * majorSkip < 60.0) {
  // majorDivisors = new double[3] { 2.0, 2.5, 2.0 };
  majorSkip *= majorDivisors[majorSkipPower % majorDivisors.length]
  majorSkipPower++

  console.log('majorSkipPower', majorSkipPower)
  console.log('majorSkip', majorSkip)
}


console.log('return: majorSkipPower', majorSkipPower)
console.log('return: majorSkip', majorSkip)