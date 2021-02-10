import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { 
  GradeAddViewModel, 
  GradeDragDropModel, 
  GradeScaleAddViewModel, 
  GradeScaleListView, 
  EffortGradeScaleListModel, 
  EffortGradeScaleModel, 
  GetAllEffortGradeScaleListModel,
   UpdateEffortGradeScaleSortOrderModel ,
   SchoolSpecificStandarModel,
   GradeStandardSubjectCourseListModel,
   GetAllSchoolSpecificListModel,
   CheckStandardRefNoModel, 
   EffortGradeLibraryCategoryListView,
   EffortGradeLibraryCategoryAddViewModel,
   EffortGradeLlibraryDragDropModel,
   EffortGradeLibraryCategoryItemAddViewModel,
   HonorRollAddViewModel,
   HonorRollListModel} from '../models/grades.model';

@Injectable({
  providedIn: 'root'
})
export class GradesService {

  apiUrl: string = environment.apiURL;
  constructor(private http: HttpClient) { }

  addEffortGradeScale(effortGradeScale:EffortGradeScaleModel){
    let apiurl = this.apiUrl + effortGradeScale._tenantName + "/Grade/addEffortGradeScale";
    return this.http.post<EffortGradeScaleModel>(apiurl, effortGradeScale)
  }

  updateEffortGradeScale(effortGradeScale:EffortGradeScaleModel){
    let apiurl = this.apiUrl + effortGradeScale._tenantName + "/Grade/updateEffortGradeScale";
    return this.http.put<EffortGradeScaleModel>(apiurl, effortGradeScale)
  }

  deleteEffortGradeScale(effortGradeScale:EffortGradeScaleModel){
    let apiurl = this.apiUrl + effortGradeScale._tenantName + "/Grade/deleteEffortGradeScale";
    return this.http.post<EffortGradeScaleModel>(apiurl, effortGradeScale)
  }

  getAllEffortGradeScaleList(effortGradeScale:GetAllEffortGradeScaleListModel){
    let apiurl = this.apiUrl + effortGradeScale._tenantName + "/Grade/getAllEffortGradeScaleList";
    return this.http.post<EffortGradeScaleListModel>(apiurl, effortGradeScale)
  }


  updateEffortGradeScaleSortOrder(effortGradeScaleSort:UpdateEffortGradeScaleSortOrderModel){
    let apiurl = this.apiUrl + effortGradeScaleSort._tenantName + "/Grade/updateEffortGradeScaleSortOrder";
    return this.http.put<UpdateEffortGradeScaleSortOrderModel>(apiurl, effortGradeScaleSort)
  }

  getAllGradeScaleList(obj: GradeScaleListView){
    let apiurl = this.apiUrl + obj._tenantName+"/Grade/getAllGradeScaleList";   
    return this.http.post<GradeScaleListView>(apiurl,obj)
  }
  addGradeScale(obj:GradeScaleAddViewModel){
    let apiurl = this.apiUrl + obj._tenantName+"/Grade/addGradeScale";
    return this.http.post<GradeScaleAddViewModel>(apiurl,obj)
  }
  updateGradeScale(obj:GradeScaleAddViewModel){
    let apiurl = this.apiUrl + obj._tenantName+"/Grade/updateGradeScale";
    return this.http.put<GradeScaleAddViewModel>(apiurl,obj)
  }
  deleteGradeScale(obj:GradeScaleAddViewModel){
    let apiurl = this.apiUrl + obj._tenantName+"/Grade/deleteGradeScale";
    return this.http.post<GradeScaleAddViewModel>(apiurl,obj)
  }
  deleteGrade(obj:GradeAddViewModel){
    let apiurl = this.apiUrl + obj._tenantName+"/Grade/deleteGrade";
    return this.http.post<GradeAddViewModel>(apiurl,obj)
  }
  addGrade(obj:GradeAddViewModel){
    let apiurl = this.apiUrl + obj._tenantName+"/Grade/addGrade";
    return this.http.post<GradeAddViewModel>(apiurl,obj)
  }
  updateGrade(obj:GradeAddViewModel){
    let apiurl = this.apiUrl + obj._tenantName+"/Grade/updateGrade";
    return this.http.put<GradeAddViewModel>(apiurl,obj)
  }
  updateGradeSortOrder(obj:GradeDragDropModel){
    let apiurl = this.apiUrl + obj._tenantName+"/Grade/updateGradeSortOrder";
    return this.http.put<GradeDragDropModel>(apiurl,obj)
  }

  // School Specific Standards
  addGradeUsStandard(schoolSpecificStandard:SchoolSpecificStandarModel){
    let apiurl = this.apiUrl + schoolSpecificStandard._tenantName+"/Grade/addGradeUsStandard";
    return this.http.post<SchoolSpecificStandarModel>(apiurl,schoolSpecificStandard)
  }
  updateGradeUsStandard(schoolSpecificStandard:SchoolSpecificStandarModel){
    let apiurl = this.apiUrl + schoolSpecificStandard._tenantName+"/Grade/updateGradeUsStandard";
    return this.http.put<SchoolSpecificStandarModel>(apiurl,schoolSpecificStandard)
  }
  deleteGradeUsStandard(schoolSpecificStandard:SchoolSpecificStandarModel){
    let apiurl = this.apiUrl + schoolSpecificStandard._tenantName+"/Grade/deleteGradeUsStandard";
    return this.http.post<SchoolSpecificStandarModel>(apiurl,schoolSpecificStandard)
  }
  
  getAllGradeUsStandardList(schoolSpecificStandard:GetAllSchoolSpecificListModel){
    let apiurl = this.apiUrl + schoolSpecificStandard._tenantName+"/Grade/getAllGradeUsStandardList";
    return this.http.post<GetAllSchoolSpecificListModel>(apiurl,schoolSpecificStandard)
  }

  getAllSubjectStandardList(schoolSpecificStandard:GradeStandardSubjectCourseListModel){
    let apiurl = this.apiUrl + schoolSpecificStandard._tenantName+"/Grade/getAllSubjectStandardList";
    return this.http.post<GradeStandardSubjectCourseListModel>(apiurl,schoolSpecificStandard)
  }

  getAllCourseStandardList(schoolSpecificStandard:GradeStandardSubjectCourseListModel){
    let apiurl = this.apiUrl + schoolSpecificStandard._tenantName+"/Grade/getAllCourseStandardList";
    return this.http.post<GradeStandardSubjectCourseListModel>(apiurl,schoolSpecificStandard)
  }
  checkStandardRefNo(schoolSpecificStandard: CheckStandardRefNoModel) {
    let apiurl = this.apiUrl + schoolSpecificStandard._tenantName + "/Grade/checkStandardRefNo";
    return this.http.post<CheckStandardRefNoModel>(apiurl, schoolSpecificStandard)
  }


  getAllEffortGradeLlibraryCategoryList(obj:EffortGradeLibraryCategoryListView){
    let apiurl = this.apiUrl + obj._tenantName+"/Grade/getAllEffortGradeLlibraryCategoryList";
    return this.http.post<EffortGradeLibraryCategoryListView>(apiurl,obj)
  }
  deleteEffortGradeLibraryCategoryItem(obj:EffortGradeLibraryCategoryItemAddViewModel){
    let apiurl = this.apiUrl + obj._tenantName+"/Grade/deleteEffortGradeLibraryCategoryItem";
    return this.http.post<EffortGradeLibraryCategoryItemAddViewModel>(apiurl,obj)
  }
  deleteEffortGradeLibraryCategory(obj){
    let apiurl = this.apiUrl + obj._tenantName+"/Grade/deleteEffortGradeLibraryCategory";
    return this.http.post<EffortGradeLibraryCategoryAddViewModel>(apiurl,obj)
  }
  addEffortGradeLibraryCategoryItem(obj:EffortGradeLibraryCategoryItemAddViewModel){
    let apiurl = this.apiUrl + obj._tenantName+"/Grade/addEffortGradeLibraryCategoryItem";
    return this.http.post<EffortGradeLibraryCategoryItemAddViewModel>(apiurl,obj)
  }
  updateEffortGradeLibraryCategoryItem(obj:EffortGradeLibraryCategoryItemAddViewModel){
    let apiurl = this.apiUrl + obj._tenantName+"/Grade/updateEffortGradeLibraryCategoryItem";
    return this.http.put<EffortGradeLibraryCategoryItemAddViewModel>(apiurl,obj)
  }
  addEffortGradeLibraryCategory(obj:EffortGradeLibraryCategoryAddViewModel){
    let apiurl = this.apiUrl + obj._tenantName+"/Grade/addEffortGradeLibraryCategory";
    return this.http.post<EffortGradeLibraryCategoryAddViewModel>(apiurl,obj)
  }
  updateEffortGradeLibraryCategory(obj:EffortGradeLibraryCategoryAddViewModel){
    let apiurl = this.apiUrl + obj._tenantName+"/Grade/updateEffortGradeLibraryCategory";
    return this.http.put<EffortGradeLibraryCategoryAddViewModel>(apiurl,obj)
  }
  updateEffortGradeLlibraryCategorySortOrder(obj:EffortGradeLlibraryDragDropModel){
    let apiurl = this.apiUrl + obj._tenantName+"/Grade/updateEffortGradeLlibraryCategorySortOrder";
    return this.http.put<EffortGradeLlibraryDragDropModel>(apiurl,obj)
  }


  //honor setup
  addHonorRoll(obj:HonorRollAddViewModel){
    let apiurl = this.apiUrl + obj._tenantName+"/Grade/addHonorRoll";
    return this.http.post<HonorRollAddViewModel>(apiurl,obj);
  }
  updateHonorRoll(obj:HonorRollAddViewModel){
    let apiurl = this.apiUrl + obj._tenantName+"/Grade/updateHonorRoll";
    return this.http.put<HonorRollAddViewModel>(apiurl,obj);
  }
  deleteHonorRoll(obj:HonorRollAddViewModel){
    let apiurl = this.apiUrl + obj._tenantName+"/Grade/deleteHonorRoll";
    return this.http.post<HonorRollAddViewModel>(apiurl,obj);
  }
  getAllHonorRollList(obj:HonorRollListModel){
    let apiurl = this.apiUrl + obj._tenantName+"/Grade/getAllHonorRollList";
    return this.http.post<HonorRollListModel>(apiurl,obj)
  }
}