import { readFileSync } from 'fs'

export class Map {
    public destinationRangeStart: bigint;
    public sourceRangeStart: bigint;
    public range: bigint;

    public constructor(destinationRangeStart: bigint, sourceRangeStart: bigint, range: bigint) {
        this.destinationRangeStart = destinationRangeStart;
        this.sourceRangeStart = sourceRangeStart;
        this.range = range;
    }
}

export class SeedMap {

    public name: string;
    public maps: Map[];

    constructor(name: string, maps: Map[]) {
        this.name = name;
        this.maps = maps;
    }
}

export function getLines (filePath: string): string[] {
    const file: string = readFileSync(filePath, 'utf-8');
    return file.split('\n');
}

export function getSeedMaps(lines: string[]): SeedMap[] {
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
