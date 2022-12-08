use std::cmp;

fn main() {
    let data: Vec<&str> = include_str!("../input.txt")
        .split("\r\n")
        .collect::<Vec<&str>>();

    let mut grid = vec![0; 1000 * 1000];

    for pair in &data {
        let parts: Vec<&str> = pair.split(" -> ").collect();
        let first: Vec<&str> = parts[0].split(",").collect();
        let second: Vec<&str> = parts[1].split(",").collect();
        if first[0] != second[0] && first[1] != second[1] {
            continue;
        }
        if first[0] == second[0] {
            let x: usize = first[0].parse().unwrap();
            let y1: usize = cmp::min(first[1], second[1]).parse().unwrap();
            let y2: usize = cmp::max(first[1], second[1]).parse().unwrap();
            for i in y1..=y2 {
                grid[i * 1000 + x] += 1;
            }
        }
        if first[1] == second[1] {
            let y: usize = first[1].parse().unwrap();
            let x1: usize = cmp::min(first[0], second[0]).parse().unwrap();
            let x2: usize = cmp::max(first[0], second[0]).parse().unwrap();
            for i in x1..=x2 {
                grid[y * 1000 + i] += 1;
            }
        }
    }

    // for i in 0..10 {
    //     println!(
    //         "{:?}",
    //         grid.iter()
    //             .map(|x| *x)
    //             .skip(10 * i)
    //             .take(10)
    //             .collect::<Vec<i32>>()
    //     );
    // }

    grid.retain(|x| *x >= 2);
    println!("{}", grid.len());
}
