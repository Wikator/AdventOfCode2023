import {readFileSync} from "fs";

enum Direction {
    Up,
    Down,
    Right,
    Left,
    None
}

class Distance {
    number: number
    inARow: number
    direction: Direction;

    constructor(number: number, inARow: number, direction: Direction) {
        this.inARow = inARow;
        this.number = number;
        this.direction = direction;
    }
}

function dijkstra2D(grid: number[][]): number {
    const INF: number = Number.MAX_SAFE_INTEGER;
    const rows: number = grid.length;
    const cols: number = grid[0].length;
    const distances: Distance[][] = new Array(rows);
    for (let i = 0; i < rows; i++) {
        distances[i] = new Array(cols);
        for (let j = 0; j < cols; j++) {
            distances[i][j] = new Distance(INF, 0, Direction.None);
        }
    }
    const visited: boolean[][] = new Array(rows).fill([]).map(() => new Array(cols).fill(false));

    distances[0][0] = new Distance(0, 0, Direction.None);

    for (let i: number = 0; i < rows * cols - 1; i++) {
        const [row, col] = minDistance2D(distances, visited);
        visited[row][col] = true;
        // console.log(distances)
        let r;
        let c;

        r = row - 1;
        c = col;
        if (r >= 0 && r < rows && c >= 0 && c < cols && !visited[r][c]) {
            if (distances[row][col].direction == Direction.Up) {
                if (distances[row][col].inARow < 3) {
                    const newDistance: number = distances[row][col].number + grid[r][c];
                    if (newDistance < distances[r][c].number) {
                        distances[r][c].direction = Direction.Up;
                        distances[r][c].number = newDistance;
                        distances[r][c].inARow = distances[row][col].inARow + 1;
                    }
                }
            } else {
                const newDistance: number = distances[row][col].number + grid[r][c];
                if (newDistance < distances[r][c].number) {
                    distances[r][c].number = newDistance;
                    distances[r][c].direction = Direction.Up;
                    distances[r][c].inARow = 1;
                }
            }

        }

        r = row + 1;
        c = col;
        if (r >= 0 && r < rows && c >= 0 && c < cols && !visited[r][c]) {
            if (distances[row][col].direction == Direction.Down) {
                if (distances[row][col].inARow < 3) {
                    const newDistance: number = distances[row][col].number + grid[r][c];
                    if (newDistance < distances[r][c].number) {
                        distances[r][c].direction = Direction.Down;
                        distances[r][c].number = newDistance;
                        distances[r][c].inARow = distances[row][col].inARow + 1;
                    }
                }
            } else {
                const newDistance: number = distances[row][col].number + grid[r][c];
                if (newDistance < distances[r][c].number) {
                    distances[r][c].number = newDistance;
                    distances[r][c].direction = Direction.Down;
                    distances[r][c].inARow = 1;
                }
            }
        }

        r = row;
        c = col - 1;
        if (r >= 0 && r < rows && c >= 0 && c < cols && !visited[r][c]) {
            if (distances[row][col].direction == Direction.Left) {
                if (distances[row][col].inARow < 3) {
                    const newDistance: number = distances[row][col].number + grid[r][c];
                    if (newDistance < distances[r][c].number) {
                        distances[r][c].direction = Direction.Left;
                        distances[r][c].number = newDistance;
                        distances[r][c].inARow = distances[row][col].inARow + 1;
                    }
                }
            } else {
                const newDistance: number = distances[row][col].number + grid[r][c];
                if (newDistance < distances[r][c].number) {
                    distances[r][c].number = newDistance;
                    distances[r][c].direction = Direction.Left;
                    distances[r][c].inARow = 1;
                }
            }
        }

        r = row;
        c = col + 1;
        if (r >= 0 && r < rows && c >= 0 && c < cols && !visited[r][c]) {
            if (distances[row][col].direction == Direction.Right) {
                if (distances[row][col].inARow < 3) {
                    const newDistance: number = distances[row][col].number + grid[r][c];
                    if (newDistance < distances[r][c].number) {
                        distances[r][c].direction = Direction.Right;
                        distances[r][c].number = newDistance;
                        distances[r][c].inARow = distances[row][col].inARow + 1;
                    }
                }
            } else {
                const newDistance: number = distances[row][col].number + grid[r][c];
                if (newDistance < distances[r][c].number) {
                    distances[r][c].number = newDistance;
                    distances[r][c].direction = Direction.Right;
                    distances[r][c].inARow = 1;
                }
            }
        }
    }
    console.log(distances)
    return distances[rows - 1][cols - 1].number;
}

function minDistance2D(distances: Distance[][], visited: boolean[][]): [number, number] {
    let min: number = Number.MAX_SAFE_INTEGER;
    let minIndex: [number, number] = [-1, -1];

    for (let i: number = 0; i < distances.length; i++) {
        for (let j: number = 0; j < distances[i].length; j++) {
            if (!visited[i][j] && distances[i][j].number < min) {
                min = distances[i][j].number;
                minIndex = [i, j];
            }
        }
    }
    return minIndex;
}

export function getLines (filePath: string): number[][] {
    const file: string = readFileSync(filePath, 'utf-8');
    return file.split('\r\n').map(line => line.split('').map(c => parseInt(c)));
}

const lines: number[][] = getLines('data/input.txt');
console.log(dijkstra2D(lines))
