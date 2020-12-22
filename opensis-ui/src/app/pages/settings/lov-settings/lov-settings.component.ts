import { Component, OnInit } from '@angular/core';
import { fadeInRight400ms } from '../../../../@vex/animations/fade-in-right.animation';

@Component({
  selector: 'vex-lov-settings',
  templateUrl: './lov-settings.component.html',
  styleUrls: ['./lov-settings.component.scss'],
  animations: [
    fadeInRight400ms
  ]
})
export class LovSettingsComponent implements OnInit {
  pages=['School Level', 'School Classification', 'Countries', 'Female Toilet Type', 'Male Toilet Type', 'Common Toilet Type', 'Race', 'Ethnicity', 'Language']
  parentSettings=true;
  pageTitle = 'School Level';
  pageId: string = '';
  displaySchoolLevel = true;
  displaySchoolClassification = false;
  displayCountries = false;
  displayFemaleToiletType = false;
  displayMaleToiletType = false;
  displayCommonToiletType = false;
  displayRace = false;
  displayEthnicity = false;
  displayLanguage = false;

  SchoolLevelFlag: boolean = true;
  SchoolClassificationFlag: boolean = false;
  CountriesFlag: boolean = false;
  FemaleToiletTypeFlag: boolean = false;
  MaleToiletTypeFlag: boolean = false;
  CommonToiletTypeFlag: boolean = false;
  RaceFlag: boolean = false;
  EthnicityFlag: boolean = false;
  LanguageFlag: boolean = false;

  constructor() { }

  ngOnInit(): void {
    this.pageId = localStorage.getItem("pageId");
  }

  getSelectedPage(pageId){
    this.pageId = pageId;
    localStorage.setItem("pageId", pageId);
  }

}
