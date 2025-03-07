"use client";

import axios from "axios";
import { useEffect } from "react";
import DropdownSelect from "./components/shared/DropdownSelect";
import { DropdownSelectOption } from "./types/DropdownSelectOption";
import PersonService from "./services/PersonService";
import { Person } from "./types/Person";

const options: DropdownSelectOption[] = [];

export default function Home() {
  useEffect(() => {
    PersonService.GetPeople().then((people) => {
      people.map((p: Person) => {
        options.push({ label: `${p.fullName}`, value: p.id });
      });
    });
  }, []);

  return (
    <div className="grid grid-rows-[20px_1fr_20px] items-center justify-items-center min-h-screen p-8 pb-20 gap-16 sm:p-20 font-[family-name:var(--font-geist-sans)]">
      <DropdownSelect
        label="Select a person"
        options={options}
        onChange={(value) => console.log(value)}
      ></DropdownSelect>
    </div>
  );
}
