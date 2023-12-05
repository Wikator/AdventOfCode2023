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

const ans: bigint = seeds.reduce((ansAcc: bigint, seed: bigint): bigint => {
    const newSeed: bigint = seedMaps.reduce((seedMapAcc: bigint, seedMap: SeedMap): bigint => {
        return seedMap.maps.reduce((mapAcc: bigint, map: Map): bigint => {
            if (mapAcc != seedMapAcc)
                return mapAcc;

            if (mapAcc > map.sourceRangeStart + map.range - BigInt(1) || mapAcc < map.sourceRangeStart)
                return mapAcc;

            const distance: bigint =  mapAcc - map.sourceRangeStart;
            return map.destinationRangeStart + distance;
        }, seedMapAcc);
    }, seed);

    if (!ansAcc)
        return newSeed;

    if (newSeed < ansAcc) return newSeed;
    return ansAcc;
}, null)

console.log(ans);
