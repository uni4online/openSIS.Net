
export interface YEAR {
    id: string;
    name: string;
  }

  export interface PART {
    id: string;
    name: string;
  }
  
  
  /** list of years */
  export const YEARS: YEAR[] = [
    {name: '2020-2021', id: 'E'},
  ];

   /** list of parts */
   export const PARTS: PART[] = [
    {name: 'Full Year', id: 'A'},
    // {name: 'Semester 1', id: 'B'},
    // {name: 'Semester 2', id: 'C'},
    // {name: 'Quarter 1', id: 'D'},
    // {name: 'Quarter 2', id: 'E'},
    // {name: 'Quarter 3', id: 'F'},
    // {name: 'Quarter 4', id: 'G'},
  ];