import {readFileSync} from "fs";

export interface Pair {
    galaxy1YIndex: number,
    galaxy1XIndex: number,
    galaxy2YIndex: number,
    galaxy2XIndex: number
}

export function getPairs(galaxies: string[]): Pair[] {
    const pairs: Pair[] = [];

    for (let i: number = 0; i < galaxies.length; i++) {
        for (let j: number = 0; j < galaxies[i].length; j++) {
            if (galaxies[i][j] === '#') {
                for (let z: number = i; z < galaxies.length; z++) {
                    for (let l: number = (i === z) ? j + 1 : 0; l < galaxies[z].length; l++) {
                        if (galaxies[z][l] === '#') {
                            pairs.push({galaxy1YIndex: i, galaxy1XIndex: j, galaxy2YIndex: z, galaxy2XIndex: l})
                        }
                    }
                }
            }
        }
    }

    return pairs
}

export function getLines (filePath: string): string[] {
    const file: string = readFileSync(filePath, 'utf-8');
    return file.split('\r\n');
}
