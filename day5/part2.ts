import { readFileSync } from 'fs'

class Map {
    public destinationRangeStart: bigint;
    public sourceRangeStart: bigint;
    public range: bigint;

    public constructor(destinationRangeStart: bigint, sourceRangeStart: bigint, range: bigint) {
        this.destinationRangeStart = destinationRangeStart;
        this.sourceRangeStart = sourceRangeStart;
        this.range = range;
    }
}

class SeedMap {

    public name: string;
    public maps: Map[];

    constructor(name: string, maps: Map[]) {
        this.name = name;
        this.maps = maps;
    }
}

function getLines (filePath: string): string[] {
    const file: string = readFileSync(filePath, 'utf-8');
    return file.split('\n');
}

function getSeedMaps(lines: string[]): SeedMap[] {
    const seedMaps: string[][] = lines.reduce((acc: string[][], curr: string): string[][] => {
        if (curr === '\r') {
            acc.push([]);
            return acc;
        } else {
            acc[acc.length - 1].push(curr);
            return acc;
        }
    }, [[]]);

    return seedMaps.map((s: string[]) => {
        const name: string = s.shift().split(' ')[0];
        const maps: Map[] = s.map((m: string) => {
            const splitLine: string[] = m.split(' ');
            const destinationRangeStart: bigint = BigInt(splitLine[0]);
            const sourceRangeStart: bigint = BigInt(splitLine[1]);
            const range: bigint = BigInt(splitLine[2]);
            return new Map(destinationRangeStart, sourceRangeStart, range);
        });

        return new SeedMap(name,maps);
    });
}

const lines: string[] = getLines('./data/input.txt');
const seeds: bigint[] = lines[0].split(' ').slice(1).map((s: string) => BigInt(s));
const seedMaps: SeedMap[] = getSeedMaps(lines.splice(2));
const groupedSeeds: bigint[][] = [];

for (let i: number = 0; i < seeds.length; i += 2) {
    groupedSeeds.push([seeds[i], seeds[i + 1]]);
}

let ans = null;

console.log(groupedSeeds)

groupedSeeds.forEach(groupedSeed => {
    for (let i: bigint = BigInt(0); i < groupedSeed[1]; i += BigInt(1)) {

        const newSeed: bigint = seedMaps.reduce((seedMapAcc: bigint, seedMap: SeedMap): bigint => {
            let newValue = seedMapAcc;
            seedMap.maps.every((map: Map) => {

                if (newValue > map.sourceRangeStart + map.range - BigInt(1) || newValue < map.sourceRangeStart)
                    return true;

                const distance: bigint =  seedMapAcc - map.sourceRangeStart;
                newValue = map.destinationRangeStart + distance;
                return false;
            }, seedMapAcc);
            return newValue;
        }, groupedSeed[0] + i);

        if (!ans)
            ans = newSeed;

        if (newSeed < ans) ans = newSeed;
    }
    console.log('pair done')
})

console.log(ans);
